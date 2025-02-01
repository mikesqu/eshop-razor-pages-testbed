using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MadStick.Models.DataModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Utilities;

namespace MadStickWebAppTester.Data
{
    public class MadStickContext : IdentityDbContext<ApplicationUser>
    {
        public MadStickContext(DbContextOptions<MadStickContext> options)
            : base(options)
        {
        }

        public DbSet<MadStickProduct> MadStickProducts { get; set; }

        public DbSet<StorageUnit> StorageUnits { get; set; }

        public DbSet<StorageProduct> StorageProducts { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartProduct> CartProducts { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<MadStickWebAppTester.Data.UserEntity.EndpointPermission> EndpointPermissions { get; set; }

        public DbSet<MadStickWebAppTester.Data.UserEntity.Endpoint> Endpoints { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<MadStickProduct>()
            //.HasIndex(p => p.SlugName)
            //.IsUnique();

            builder.Entity<MadStickProduct>()
                .Property(p => p.SlugName)
                .ValueGeneratedOnAddOrUpdate()
                .HasValueGenerator(typeof(SlugGenerator));




            base.OnModelCreating(builder);
        }
    }
}
