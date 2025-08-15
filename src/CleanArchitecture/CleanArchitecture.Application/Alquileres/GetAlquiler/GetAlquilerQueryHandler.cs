using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Alquileres.GetAlquiler
{
    /// <summary>
    /// Manejador de la consulta <see cref="GetAlquilerQuery"/> que recupera un alquiler desde la base de datos utilizando Dapper.
    /// </summary>
    internal sealed record GetAlquilerQueryHandler : IQueryHandler<GetAlquilerQuery, AlquilerResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetAlquilerQueryHandler"/>.
        /// </summary>
        /// <param name="sqlConnectionFactory">Fábrica de conexiones SQL utilizada para acceder a la base de datos.</param>
        public GetAlquilerQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        /// <summary>
        /// Ejecuta la lógica para manejar la consulta de obtención de un alquiler por su identificador.
        /// </summary>
        /// <param name="request">Consulta que contiene el identificador del alquiler.</param>
        /// <param name="cancellationToken">Token de cancelación para operaciones asincrónicas.</param>
        /// <returns>
        /// Un <see cref="Result{T}"/> que contiene una instancia de <see cref="AlquilerResponse"/> si el alquiler fue encontrado; 
        /// de lo contrario, devuelve null.
        /// </returns>
        public async Task<Result<AlquilerResponse>> Handle(
            GetAlquilerQuery request,
            CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var sql = """
                SELECT
                    id AS Id,
                    vehiculo_id AS VehiculoId,
                    user_id AS UserId,
                    status AS Status,
                    precio_por_periodo AS PrecioAlquiler,
                    precio_por_periodo_tipo_moneda AS TipoMonedaAlquiler,
                    precio_mantenimiento AS PrecioMantenimiento,
                    precio_mantenimiento_tipo_moneda AS TipoMonedaMantenimiento,
                    precio_accesorios AS AccesoriosPrecio,
                    precio_accesorios_tipo_moneda AS TipoMonedaAccesorio,
                    precio_total AS PrecioTotal,
                    precio_total_tipo_moneta AS PrecioTotalTipoMoneda,
                    duracion_inicio AS DuracionInicio,
                    duracion_final AS DuracionFinal,
                    fecha_creacion AS FechaCreacion
                FROM alquileres
                WHERE id = @AlquilerId
            """;

            var alquiler = await connection.QueryFirstOrDefaultAsync<AlquilerResponse>(
                sql, new { request.AlquilerId });

            return alquiler;
        }
    }
}
