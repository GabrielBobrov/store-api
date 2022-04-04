using Npgsql.TypeMapping;
using Store.Core.Enums;

namespace Store.Infra.Configuration
{
    public static class NpgsqlConfiguration
    {
        public static void AddGlobalTypeMappers(this INpgsqlTypeMapper mapper)
        {
            mapper.MapEnum<Status>();
        }
    }
}
