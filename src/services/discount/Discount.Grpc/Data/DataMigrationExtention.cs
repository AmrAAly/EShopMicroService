using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public static class DataMigrationExtention
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            using var context = serviceScope.ServiceProvider.GetRequiredService<DiscountDbContext>();
            if (context != null)
            {
                context.Database.Migrate();
            }

            return app;
        }
    }
}
