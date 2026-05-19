using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace kamrj.EntityFrameworkCore
{
    public static class kamrjDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<kamrjDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<kamrjDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
