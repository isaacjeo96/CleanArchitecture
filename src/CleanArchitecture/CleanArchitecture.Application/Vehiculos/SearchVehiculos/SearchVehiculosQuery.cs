using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    /// <summary>
    /// Representa una consulta para obtener una lista de vehículos registrados entre dos fechas específicas.
    /// </summary>
    /// <param name="fechaInicio">Fecha inicial del rango de búsqueda.</param>
    /// <param name="fechaFin">Fecha final del rango de búsqueda.</param>
    public sealed record SearchVehiculosQuery(
        DateOnly fechaInicio,
        DateOnly fechaFin
    ) : IQuery<IReadOnlyList<VehiculoResponse>>;
}
