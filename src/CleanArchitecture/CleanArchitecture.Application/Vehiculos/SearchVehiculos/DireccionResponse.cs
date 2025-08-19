namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    /// <summary>
    /// Representa una dirección geográfica asociada a un vehículo,
    /// incluyendo país, departamento, provincia y calle.
    /// </summary>
    public sealed class DireccionResponse
    {
        /// <summary>
        /// Obtiene el nombre del país donde se encuentra la dirección.
        /// </summary>
        public string? Pais { get; init; }

        /// <summary>
        /// Obtiene el nombre del departamento o estado dentro del país.
        /// </summary>
        public string? Departamento { get; init; }

        /// <summary>
        /// Obtiene el nombre de la provincia o subdivisión administrativa.
        /// </summary>
        public string? Provincia { get; init; }

        /// <summary>
        /// Obtiene el nombre de la calle correspondiente a la dirección.
        /// </summary>
        public string? Calle { get; init; }
    }
}
