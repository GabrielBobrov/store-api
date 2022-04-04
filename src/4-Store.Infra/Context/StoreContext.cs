using System;
using Store.Domain.Entities;
using Store.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using Store.Infra.Configuration;
using Npgsql;

namespace Store.Infra.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext()
        {
            NpgsqlConnection.GlobalTypeMapper.AddGlobalTypeMappers();
        }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuider)
        {
            optionsBuider.UseNpgsql("Server=127.0.0.1;Port=5432;Database=store;User Id=postgres;Password=gabriel123;Timeout=15;");
        }

        public virtual DbSet<Costumer> Costumers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.AddPostgresEnums();
            builder.AddMappings();

            base.OnModelCreating(builder);
        }
    }
}