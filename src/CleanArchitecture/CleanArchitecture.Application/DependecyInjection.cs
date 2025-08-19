using Microsoft.Extensions.DependencyInjection; // Necesario para IServiceCollection
using MediatR;                                  // Necesario para registrar MediatR
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Application.Abstractions.Behaivors; // Para poder registrar LoggingBehaivor

namespace CleanArchitecture.Application;

/// <summary>
/// Clase estática de configuración para registrar todos los servicios
/// necesarios dentro de la capa de aplicación. Esto incluye casos de uso,
/// servicios de dominio (como PrecioService) y herramientas como MediatR.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Método de extensión que permite registrar los servicios de la capa Application
    /// en el contenedor de inyección de dependencias (<see cref="IServiceCollection"/>).
    /// </summary>
    /// <param name="services">
    /// Contenedor de servicios donde se registrarán las dependencias.
    /// </param>
    /// <returns>
    /// El mismo contenedor <see cref="IServiceCollection"/> con las dependencias registradas.
    /// </returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // 🧩 Registro de MediatR
        services.AddMediatR(configuration =>
        {
            // 📌 Registrar todos los handlers encontrados en este ensamblado
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            // 📌 Agrega un comportamiento transversal (cross-cutting) para logging
            // que se ejecutará antes y después de cada comando que implemente IBaseCommand.
            // Esto permite llevar un registro automático de ejecución y errores.
            configuration.AddOpenBehavior(typeof(LoggingBehaivor<,>));
        });

        // 🧩 Registro de PrecioService como servicio sin estado (stateless)
        services.AddTransient<PrecioService>();

        // 🧩 Devuelve el contenedor actualizado para continuar encadenando registros si es necesario
        return services;
    }
}
