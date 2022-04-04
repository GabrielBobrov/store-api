using Microsoft.EntityFrameworkCore;
using Store.Core.Enums;
using Store.Infra.Mappings;

namespace Store.Infra.Configuration
{
    public static class EFConfigurations
    {
        public static ModelBuilder AddMappings(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CostumerMapping());
            modelBuilder.ApplyConfiguration(new OrderMapping());


            return modelBuilder;
        }

        public static ModelBuilder AddPostgresEnums(this ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Status>();
            return modelBuilder;
        }
    }
}
