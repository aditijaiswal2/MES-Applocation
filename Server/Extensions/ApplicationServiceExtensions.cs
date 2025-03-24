using DinkToPdf;
using DinkToPdf.Contracts;
using MES.Server.Services;
using Microsoft.EntityFrameworkCore;
using MES.Server.Contracts;
using MES.Server.Models;


namespace MES.Server.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<OutlookEmailService>();

            services.AddTransient<IDocumentService, DocumentService>();

           
            return services;
        }
    }
}