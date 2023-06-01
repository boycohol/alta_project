using AltaProject.Data;
using AltaProject.Helper.Email;
using AltaProject.Repository.Implement;
using AltaProject.Repository;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using AltaProject.Service;
using AltaProject.Service.Implement;
using Quartz;
using AltaProject.Helper.JobScheduler;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace AltaProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Add Jwt in swaggerGen
            builder.Services.AddSwaggerGen();

            //Add AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            //Add Repositories
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IVisitPlanRepository, VisitPlanRepository>();
            builder.Services.AddTransient<INotificationRepository, NotificationRepository>();
            builder.Services.AddTransient<ITaskRepository, TaskRepository>();
            builder.Services.AddTransient<ICommentRepository, CommentRepository>();
            builder.Services.AddTransient<ISurveyRepository, SurveyRepository>();
            builder.Services.AddTransient<IArticleRepository, ArticleRepository>();
            builder.Services.AddTransient<IAreaRepository, AreaRepository>();

            //Add services
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddTransient<IHashPassword, HashPassword>();
            builder.Services.AddTransient<ITokenService, TokenService>();

            //Add Quartz - scheduler
            builder.Services.AddQuartz(q =>
            {
                var jobKey = new JobKey("Reminder");

                q.AddJob<BackgroundJob>(option =>
                {
                    option.WithIdentity(jobKey);
                });

                q.AddTrigger(option =>
                {
                    option.ForJob(jobKey)
                    .WithIdentity("ReminderTrigger")
                    .WithCronSchedule("0 0 23 * * ?");
                });

                q.UseMicrosoftDependencyInjectionJobFactory();
            });
            builder.Services.AddQuartzHostedService(option =>
            {
                option.WaitForJobsToComplete = true;
            });

            //Add Email Configs
            builder.Services.AddSingleton(builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            //Connect to database
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseNpgsql(builder.Configuration.GetConnectionString("AltaProjectDatabase"));
            });
            //Authentication with Token

            //Jwt Token 
            /*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration, "AzureAd");*/
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration)
            .EnableTokenAcquisitionToCallDownstreamApi()
            .AddInMemoryTokenCaches()
            .AddMicrosoftGraph(x =>
            {
                string tenantId = builder.Configuration.GetValue<string>("AzureAd:TenantId");
                string clientId = builder.Configuration.GetValue<string>("AzureAd:ClientId");
                string clientSecret = builder.Configuration.GetValue<string>("AzureAd:ClientSecret");
                ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                return new GraphServiceClient(clientSecretCredential);
            }, new string[] { ".default" });

            //Enable CORS
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("NuxtJsConnect", policy =>
                {
                    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(/*c =>
                {
                    c.OAuthClientId(builder.Configuration["SwaggerAzureAd:ClientId"]);
                    c.OAuthUsePkce();
                    c.OAuthScopeSeparator(" ");
                }*/);
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}