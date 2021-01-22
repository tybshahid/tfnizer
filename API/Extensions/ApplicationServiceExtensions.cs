using API.Services;
using API.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins(new string[] { "http://localhost:3000", "test" });
                });
            });

            services.AddScoped<IValidatorService, Validator>();
            services.AddScoped<IValidatorService, MockValidatorPass>();
            services.AddScoped<IValidatorService, MockValidatorFail>();

            return services;
        }
    }
}