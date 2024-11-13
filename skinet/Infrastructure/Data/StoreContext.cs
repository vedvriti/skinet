using System;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Config;

//Infrastructure - communicates to the databse to get the data from databse
namespace Infrastructure.Data;
//DbContext class we get it from entity framework core

    public class StoreContext : DbContext
    {
        // Constructor to pass options to the base DbContext class
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        // Define DbSet for Product entity
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Telling the store context where these configurations are loacated
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }
    }
