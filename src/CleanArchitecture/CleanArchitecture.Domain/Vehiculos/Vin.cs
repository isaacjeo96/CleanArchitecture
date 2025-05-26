namespace CleanArchitecture.Domain.Vehiculos
{
    /// <summary>
    /// Representa el número de identificación vehicular (VIN) como un objeto de valor.
    /// Este valor identifica de forma única a un vehículo según estándares internacionales.
    /// </summary>
    /// <param name="Value">Cadena de texto que representa el VIN.</param>
    public record Vin(string Value);
}
