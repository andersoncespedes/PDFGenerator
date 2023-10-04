using System.Text;
using Application.UnitOfWork;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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