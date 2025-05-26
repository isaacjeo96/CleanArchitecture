namespace CleanArchitecture.Domain.Vehiculos
{
    /// <summary>
    /// Representa una dirección geográfica como un objeto de valor dentro del dominio del vehículo.
    /// No tiene identidad propia y se identifica por la combinación de sus valores.
    /// </summary>
    /// <param name="Pais">Nombre del país donde se ubica la dirección.</param>
    /// <param name="Departamento">Departamento o región administrativa.</param>
    /// <param name="Provincia">Provincia donde se encuentra la dirección.</param>
    /// <param name="Ciudad">Ciudad de ubicación.</param>
    /// <param name="Calle">Nombre de la calle o avenida.</param>
    public record Direccion(
        string Pais,
        string Departamento,
        string Provincia,
        string Ciudad,
        string Calle
    );
}
