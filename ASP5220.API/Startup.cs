using ASP5220.API.Core;
using ASP5220.API.Extensions;
using ASP5220.Application;
using ASP5220.Application.Emails;
using ASP5220.Application.Logging;
using ASP5220.Application.UseCases.Commands;
using ASP5220.Application.UseCases.DTO;
using ASP5220.Application.UseCases.Queries;
using ASP5220.DataAccess;
using ASP5220.Implementation;
using ASP5220.Implementation.Emails;
using ASP5220.Implementation.Logging;
using ASP5220.Implementation.UseCases.Commands;
using ASP5220.Implementation.UseCases.Queries;
using ASP5220.Implementation.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ASP5220.API.Core.TokenStorage;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ASP5220.Application.UseCases;
using System.IO;

namespace ASP5220.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = new AppSettings();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:8080", "http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            Configuration.Bind(appSettings);
            services.AddTransient<ITokenStorage, InMemoryTokenStorage>();

            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<ASPContext>();
                var tokenStorage = x.GetService<ITokenStorage>();
                return new JwtManager(context, appSettings.Jwt.Issuer, appSettings.Jwt.SecretKey, appSettings.Jwt.DurationSeconds, tokenStorage);
            });


            services.AddTransient<ASPContext>(x =>
            {
                DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
                builder.UseSqlServer(@"Data Source=DESKTOP-SH0FKJA\SQLEXPRESS;Initial Catalog=ASP;Integrated Security=True");
                return new ASPContext(builder.Options);
            });

            services.AddHttpContextAccessor();
            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();

                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var claimsUser = accessor.HttpContext.User;

                if(claimsUser == null || claimsUser.FindFirst("Id") == null)
                {
                    return new AnonymusActor();
                }

                var data = header.ToString().Split("Bearer ");

                if (data.Length < 2)
                {
                    throw new UnauthorizedAccessException();
                }

                var handler = new JwtSecurityTokenHandler();

                var tokenObj = handler.ReadJwtToken(data[1].ToString());

                var claims = tokenObj.Claims;

                var email = claims.First(x => x.Type == "Email").Value;
                var id = claims.First(x => x.Type == "Id").Value;
                var username = claims.First(x => x.Type == "Username").Value;
                var useCases = claims.First(x => x.Type == "UseCases").Value;
                var role = claims.First(x => x.Type == "Role").Value;

                List<int> useCaseIds = JsonConvert.DeserializeObject<List<int>>(useCases);

                return new JwtActor
                {
                    Email = email,
                    AllowedUseCases = useCaseIds,
                    Id = int.Parse(id),
                    Username = username,
                    Role = role
                };
            });

            //SERVICES REGISTRATION
            services.AddTransient<IGetMakesQuery, EFGetMakesQuery>();
            services.AddTransient<ICreateMakeCommand,EFCreateMakeCommand>();
            services.AddTransient<IRegisterCommand,EFRegisterCommand>();
            services.AddTransient<IExceptionLogger, ConsoleExceptionLogger>();
            services.AddTransient<IEmailSender,FakeEmailSender>();
            services.AddTransient<UseCaseHandler>();
            services.AddTransient<IUseCaseLogger,UseCaseLogger>();
            services.AddTransient<IGetSpecificationsQuery, EFGetSpecificationsQuery>();
            services.AddTransient<ICreateCarCommand, EFCreateCarCommand>();
            services.AddTransient<IGetCarsQuery, EFGetCarsQuery>();
            services.AddTransient<IGetSingleCarQuery,EFGetSingleCarQuery>();
            services.AddTransient<IEditCarCommand,EFEditCarCommand>();
            services.AddTransient<IDeleteCarCommand, EFDeleteCarCommand>();
            services.AddTransient<IGetAuditLogsQuery,EFGetAuditLogsQuery>();
            services.AddTransient<IFollowCarCommand, EFFollowCarCommand>();
            services.AddTransient<IUnfollowCarCommand,EFUnfollowCarCommand>();
            services.AddTransient<IDeleteUserCommand,EFDeleteUserCommand>();
            services.AddTransient<IGetUsersQuery,EFGetUsersQuery>();
            services.AddTransient<IGetSingleUserQuery,EFGetSingleUserQuery>();

            services.AddControllers();

            //VALIDATORI
            services.AddTransient<CreateMakeValidator>();
            services.AddTransient<RegisterValidator>();
            services.AddTransient<CreateCarValidator>();
            services.AddTransient<FollowCarValidator>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP5220.API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ASP5220.API.xml"));
            });

            services.AddJwt(appSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP5220.API v1"));
            }

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
