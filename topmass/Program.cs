namespace topmass
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using Quartz;
    using System.Text;
    using Topmass.Bussiness.Company;
    using Topmass.core.Business;
    using Topmass.Core.Common;
    using Topmass.CV.Business;
    using Topmass.Job.Business;
    using Topmass.Sync.Busines;
    using TopMass.Web.Business;
    using VS.core.API.job;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigBusiness();
            builder.Services.ConfigBusinessWeb();
            builder.Services.ConfigLocationBusiness();

            builder.Services.ConfigCVBusiness();
            builder.Services.ConfigJobBusiness();
            builder.Services.ConfigCompanyBusiness();
            builder.Services.ConfigCoreCommon();
            builder.Services.ConfigSyncBusiness();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            builder.Services.AddAuthorization();

            builder.Services.AddQuartz(q =>
            {
                var jobKey = new JobKey("UpdateSyncJob");
                q.AddJob<SyncJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("UpdateSyncJob-trigger")
                    //This Cron interval can be described as "run every minute"(when second is zero)
                    .WithCronSchedule("0 0/3 0 ? * * *")
                );
            });

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
