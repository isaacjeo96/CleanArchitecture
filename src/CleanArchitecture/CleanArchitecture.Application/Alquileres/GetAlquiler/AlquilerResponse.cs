namespace CleanArchitecture.Application.Alquileres.GetAlquiler
{
    /// <summary>
    /// Representa la respuesta detallada de un alquiler, incluyendo información del usuario, vehículo, costos y duración.
    /// </summary>
    public sealed class AlquilerResponse
    {
        /// <summary>
        /// Identificador único del alquiler.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Identificador del usuario que realizó el alquiler.
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// Identificador del vehículo alquilado.
        /// </summary>
        public Guid VehiculoId { get; init; }

        /// <summary>
        /// Estado actual del alquiler (por ejemplo: activo, cancelado, finalizado).
        /// </summary>
        public int Status { get; init; }

        /// <summary>
        /// Costo base del alquiler del vehículo.
        /// </summary>
        public decimal PrecioAlquiler { get; init; }

        /// <summary>
        /// Tipo de moneda utilizada para el precio de alquiler (por ejemplo: USD, EUR).
        /// </summary>
        public string? TipoMonedaAlquiler { get; init; }

        /// <summary>
        /// Costo de mantenimiento asociado al alquiler.
        /// </summary>
        public decimal PrecioMantenimiento { get; init; }

        /// <summary>
        /// Tipo de moneda utilizada para el mantenimiento.
        /// </summary>
        public string? TipoMonedaMantenimiento { get; init; }

        /// <summary>
        /// Costo total de los accesorios incluidos en el alquiler.
        /// </summary>
        public decimal AccesoriosPrecio { get; init; }

        /// <summary>
        /// Tipo de moneda utilizada para los accesorios.
        /// </summary>
        public string? TopoMonedaAccesorio { get; init; }

        /// <summary>
        /// Precio total del alquiler (alquiler + mantenimiento + accesorios).
        /// </summary>
        public decimal PrecioTotal { get; init; }

        /// <summary>
        /// Tipo de moneda utilizada para el precio total.
        /// </summary>
        public string? PrecioTotalTipoMoneda { get; init; }

        /// <summary>
        /// Fecha de inicio del periodo de alquiler.
        /// </summary>
        public DateOnly DuracionInicio { get; init; }

        /// <summary>
        /// Fecha de finalización del periodo de alquiler.
        /// </summary>
        public DateOnly DuracionFinal { get; init; }

        /// <summary>
        /// Fecha y hora de creación del registro de alquiler.
        /// </summary>
        public DateTime FechaCreacion { get; init; }
    }
}
