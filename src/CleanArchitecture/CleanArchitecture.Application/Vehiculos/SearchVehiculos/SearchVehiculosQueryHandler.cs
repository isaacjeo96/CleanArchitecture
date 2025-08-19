using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using Dapper;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    /// <summary>
    /// Manejador de la consulta <see cref="SearchVehiculosQuery"/> que recupera una lista de vehículos
    /// disponibles entre un rango de fechas determinado, excluyendo aquellos con alquileres activos.
    /// </summary>
    internal sealed class SearchVehiculosQueryHandler : IQueryHandler<SearchVehiculosQuery, IReadOnlyList<VehiculoResponse>>
    {
        /// <summary>
        /// Representa los estados de alquiler considerados como activos (reservado, confirmado, completado).
        /// </summary>
        private static readonly int[] ActiveAlquilerStatuses =
        {
            (int)AlquilerStatus.Reservado,
            (int)AlquilerStatus.Confirmado,
            (int)AlquilerStatus.Completado
        };

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        /// <summary>
        /// Inicializa una nueva instancia del <see cref="SearchVehiculosQueryHandler"/>.
        /// </summary>
        /// <param name="sqlConnectionFactory">Fábrica de conexiones SQL usada para acceder a la base de datos.</param>
        public SearchVehiculosQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        /// <summary>
        /// Maneja la ejecución de la consulta de búsqueda de vehículos disponibles.
        /// </summary>
        /// <param name="request">La consulta que contiene el rango de fechas.</param>
        /// <param name="cancellationToken">Token de cancelación para operaciones asíncronas.</param>
        /// <returns>
        /// Una lista de <see cref="VehiculoResponse"/> que representa los vehículos disponibles en el rango indicado.
        /// </returns>
        public async Task<Result<IReadOnlyList<VehiculoResponse>>> Handle(
            SearchVehiculosQuery request,
            CancellationToken cancellationToken)
        {
            // Validación de rango de fechas
            if (request.fechaInicio > request.fechaFin)
            {
                return new List<VehiculoResponse>();
            }

            using var connection = _sqlConnectionFactory.CreateConnection();

            const string sql = """
                SELECT 
                    a.id AS Id,
                    a.modelo AS Modelo,
                    a.vin AS Vin,
                    a.precio_monto AS Precio,
                    a.precio_tipo_moneda AS TipoMoneda,
                    a.direccion_pais AS Pais,
                    a.direccion_departamento AS Departamento,
                    a.direccion_provincia AS Provincia,
                    a.direccion_ciudad AS Ciudad,
                    a.direccion_calle AS Calle
                FROM vehiculos AS a
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM alquileres AS b
                    WHERE 
                        b.vehiculo_id = a.id
                        AND b.duracion_inicio <= @EndDate
                        AND b.duracion_final >= @StartDate
                        AND b.status = ANY(@ActiveAlquilerStatuses) 
                )
            """;

            var vehiculos = await connection
                .QueryAsync<VehiculoResponse, DireccionResponse, VehiculoResponse>(
                    sql,
                    (vehiculo, direccion) =>
                    {
                        vehiculo.Direccion = direccion;
                        return vehiculo;
                    },
                    new
                    {
                        StartDate = request.fechaInicio,
                        EndDate = request.fechaFin,
                        ActiveAlquilerStatuses
                    },
                    splitOn: "Pais"
                );

            return vehiculos.ToList();
        }
    }
}
