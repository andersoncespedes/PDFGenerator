using Microsoft.EntityFrameworkCore;
using Application.UnitOfWork;
using Domain.Interface;
using AutoMapper;
namespace API.Extensions
{
    public static class AplicationServicesExtensions
    {
        public static void AddAplicacionServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();


        }
    }
}