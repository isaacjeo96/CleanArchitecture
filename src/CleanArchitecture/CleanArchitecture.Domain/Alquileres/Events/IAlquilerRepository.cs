using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

/// <summary>
/// Contrato para el repositorio encargado de gestionar el acceso y persistencia de los alquileres.
/// Define las operaciones principales para consultar, verificar disponibilidad y agregar nuevos alquileres al sistema.
/// </summary>
public interface IAlquilerRepository
{
    /// <summary>
    /// Obtiene un alquiler por su identificador único.
    /// </summary>
    /// <param name="id">Identificador único del alquiler.</param>
    /// <param name="cancellationToken">Token para cancelar la operación si es necesario.</param>
    /// <returns>El alquiler correspondiente o <c>null</c> si no se encuentra.</returns>
    Task<Alquiler>? GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe un alquiler que se solapa (overlap) con el rango de fechas especificado para un vehículo dado.
    /// Útil para validar la disponibilidad antes de reservar.
    /// </summary>
    /// <param name="vehiculo">Vehículo que se desea alquilar.</param>
    /// <param name="duracion">Rango de fechas del alquiler solicitado.</param>
    /// <param name="cancellationToken">Token para cancelar la operación si es necesario.</param>
    /// <returns><c>true</c> si hay conflicto de fechas (overlap), <c>false</c> en caso contrario.</returns>
    Task<bool> IsOverlappingAsync(Vehiculo vehiculo, DateRange duracion, CancellationToken cancellationToken = default);

    /// <summary>
    /// Agrega un nuevo alquiler al contexto de persistencia.
    /// El cambio no se guarda en la base de datos hasta confirmar la transacción (por ejemplo, mediante UnitOfWork).
    /// </summary>
    /// <param name="alquiler">Instancia del alquiler a agregar.</param>
    void Add(Alquiler alquiler);
}
