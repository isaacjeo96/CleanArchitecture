namespace CleanArchitecture.Domain.Vehiculos
{
    /// <summary>
    /// Representa el modelo de un vehículo como un objeto de valor.
    /// Encapsula un valor de texto que describe el modelo (e.g., "Toyota Corolla").
    /// </summary>
    /// <param name="Value">Texto que describe el modelo del vehículo.</param>
    public record Modelo(string Value);
}
