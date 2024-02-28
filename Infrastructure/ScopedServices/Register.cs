using Application.IServices;
using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Infrastructure.ScopedServices
{
    public static class ScopedServices
    {
        // Automates the registration of services that implement specific interfaces
        public static void AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IClassromService, ClassromService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IFinancialHistoryService, FinancialHistoryService>();
            services.AddScoped<IListActivityService, ListActivityService>();
            services.AddScoped<IMatterService, MatterService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISellerService, SellerService>();
            services.AddScoped<ISupportService, SupportService>();
        }
    }
}
