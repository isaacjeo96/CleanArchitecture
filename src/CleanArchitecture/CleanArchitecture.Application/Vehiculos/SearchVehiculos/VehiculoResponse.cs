namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    /// <summary>
    /// Representa la información de un vehículo devuelta en una operación de búsqueda.
    /// Contiene detalles como modelo, VIN, precio, tipo de moneda y dirección.
    /// </summary>
    public sealed class VehiculoResponse
    {
        /// <summary>
        /// Obtiene el identificador único del vehículo.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Obtiene el nombre o descripción del modelo del vehículo.
        /// </summary>
        public string? MOdelo { get; init; }

        /// <summary>
        /// Obtiene el número de identificación vehicular (VIN).
        /// </summary>
        public string? Vin { get; init; }

        /// <summary>
        /// Obtiene el precio del vehículo.
        /// </summary>
        public decimal Precio { get; init; }

        /// <summary>
        /// Obtiene el tipo de moneda en que está expresado el precio (por ejemplo, USD, EUR).
        /// </summary>
        public string? TipoMoneda { get; init; }

        /// <summary>
        /// Obtiene o establece la dirección asociada al vehículo.
        /// </summary>
        public DireccionResponse? Direccion { get; set; }
    }
}
