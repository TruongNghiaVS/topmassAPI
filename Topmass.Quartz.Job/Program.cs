using Quartz;
using Topmass.Sync.Busines;

namespace Topmass.Quartz.Job
{


    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfigSyncBusiness();


            builder.Services.AddQuartz(q =>
            {

                var jobkey = new JobKey("SyncCVFromTopmassJob");
                q.AddJob<SyncCVFromTopmassJob>(opts => opts.WithIdentity(jobkey));

                q.AddTrigger(opts => opts
                    .ForJob(jobkey)
                    .WithIdentity("SyncCVFromTopmassJob-trigger")
                    .WithCronSchedule(" 0/50 * * * * ? *")
                );
            });
            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            var app = builder.Build();
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseAuthorization();



            app.Run();
        }
    }
}
