using Microsoft.Extensions.Logging;
using School.Data.Context;
using School.Repository;

namespace School.API.Helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<SchoolDbContext>();

                    await StoreContextSeed.SeedAsync(context, loggerFactory);



                }catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);
                }
            }

        }
    }
}
