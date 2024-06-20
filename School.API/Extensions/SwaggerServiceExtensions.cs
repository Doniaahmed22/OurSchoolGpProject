using Microsoft.OpenApi.Models;

namespace School.API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "OurSchool", Version = "v1" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization",
                    Name ="Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }

                };

                options.AddSecurityDefinition("bearer", securityScheme);

                var securityRequirements = new OpenApiSecurityRequirement
                {
                    { securityScheme, new[] { "bearer" } }
                };

                options.AddSecurityRequirement(securityRequirements);

            });


            return services;
        }
    }
}
