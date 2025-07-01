namespace CleanArchitecture.Domain.Vehiculos;

/// <summary>
/// Contrato para acceder a los datos relacionados con vehículos.
/// Se implementa en la capa de infraestructura y permite buscar un vehículo por su ID.
/// </summary>
public interface IVehiculoRepository
{
    /// <summary>
    /// Obtiene un vehículo por su identificador único.
    /// </summary>
    /// <param name="id">ID del vehículo.</param>
    /// <param name="cancellationToken">Token para cancelar la operación si es necesario.</param>
    /// <returns>Un vehículo o null si no se encuentra.</returns>
    Task<Vehiculo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
