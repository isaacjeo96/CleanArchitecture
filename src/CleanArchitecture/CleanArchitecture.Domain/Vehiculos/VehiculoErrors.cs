using CleanArchitecture.Domain.Abstractions; // Importa la clase Error para representar errores de dominio

namespace CleanArchitecture.Domain.Vehiculos;

/// <summary>
/// Contenedor estático para todos los errores de dominio relacionados con <see cref="Vehiculo"/>.
/// Facilita la reutilización y centralización de mensajes y códigos de error
/// en vez de “quemarlos” directamente en el código.
/// </summary>
public static class VehiculoErrors
{
    /// <summary>
    /// Error que indica que un vehículo con el identificador proporcionado no fue encontrado.
    /// 
    /// - Code: "Vehiculo.Found" (⚠ ver nota abajo sobre naming).
    /// - Message: "No existe un vehiculo con este id".
    /// </summary>
    public static Error NotFound = new(
        "Vehiculo.Found", // Código único que identifica el tipo de error (conviene que diga "Vehiculo.NotFound")
        "No existe un vehiculo con este id" // Mensaje descriptivo en español
    );
}
