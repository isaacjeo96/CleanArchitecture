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
        // 👉 Registra todos los handlers de MediatR que estén en este ensamblado (Application)
        services.AddMediatR(configuration =>
        {
            // ✅ Usa reflexión para buscar todas las clases que implementan interfaces de MediatR
            // como IRequestHandler<T>, INotificationHandler<T>, etc.
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        // 👉 Registra el servicio PrecioService con un ciclo de vida Transient
        // Esto significa que se creará una nueva instancia cada vez que se solicite
        services.AddTransient<PrecioService>();

        // 🔁 Devuelve el contenedor ya modificado para que pueda seguir encadenándose si se desea
        return services;
    }
}
