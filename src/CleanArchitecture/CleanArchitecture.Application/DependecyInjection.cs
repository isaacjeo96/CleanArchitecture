using Microsoft.Extensions.DependencyInjection; // Necesario para IServiceCollection
using MediatR;                                  // Necesario para registrar MediatR
using CleanArchitecture.Domain.Alquileres;     // Para poder registrar PrecioService

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
    /// en el contenedor de inyección de dependencias (IServiceCollection).
    /// </summary>
    /// <param name="services">
    /// Contenedor de servicios donde se registrarán las dependencias.
    /// </param>
    /// <returns>
    /// El mismo contenedor <see cref="IServiceCollection"/> con las dependencias registradas.
    /// </returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // 🧩 Aquí empieza el registro de MediatR
        services.AddMediatR(configuration =>
        {
            // 📌 Este método indica a MediatR que busque en este ensamblado
            // (Assembly) todos los handlers (clases que implementen IRequestHandler,
            // INotificationHandler, etc.) para que los registre automáticamente.
            // Eso permite que al hacer _mediator.Send(...) MediatR sepa qué clase manejará la petición.
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        // 🧩 Aquí registramos PrecioService en el contenedor DI
        // 👇 ¿Por qué Transient?
        // Porque PrecioService es un servicio sin estado y queremos una nueva
        // instancia cada vez que alguien la pida.
        services.AddTransient<PrecioService>();

        // 🧩 Finalmente, devolvemos el mismo IServiceCollection para que se puedan
        // seguir encadenando más llamadas si se desea (patrón Fluent)
        return services;
    }
}
