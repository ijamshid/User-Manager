using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using UserManager.Application.Services;
using UserManager.Domain.Services;

namespace UserManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(r => r.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddFluentValidationAutoValidation();
       
        return services;
    }
}