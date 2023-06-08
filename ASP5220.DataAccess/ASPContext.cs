using System;
using Microsoft.EntityFrameworkCore;
using ASP5220.Domain.Entities;

namespace ASP5220.DataAccess
{
    public class ASPContext:DbContext
    {

        public ASPContext()
        {

        }
        public ASPContext(DbContextOptions opt) : base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-SH0FKJA\SQLEXPRESS;Initial Catalog=ASP;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<RoleUseCase>().HasKey(x => new { x.RoleId, x.UseCaseId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<SpecificationValue> SpecificationValues { get; set; }
        public DbSet<SpecificationCar> SpecificationCar { get; set; }
        public DbSet<CarUser> CarFollowers { get; set; }
        public DbSet<RoleUseCase> RoleUseCases { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
