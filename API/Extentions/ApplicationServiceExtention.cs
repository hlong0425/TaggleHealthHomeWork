

using Microsoft.EntityFrameworkCore;
using Persistence;
using MediatR;
using Application.Books;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;
using Infrastructure;
using Hangfire;
using API.Services;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });


            services.AddMediatR(typeof(List.Handler));
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddSingleton<IBackgroundJobService, BackgroundJobService>();
            services.AddSignalR();

            RecurringJob.AddOrUpdate<IBackgroundJobService>("expiredBookJob", x => x.HandleExpiredBook(), "0 0 * * *");
            return services;
        }
    }
}