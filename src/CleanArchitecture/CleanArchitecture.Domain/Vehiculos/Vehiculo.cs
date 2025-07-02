using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos
{
    /// <summary>
    /// Representa un vehículo dentro del dominio del sistema.
    /// Esta entidad contiene información estructurada del vehículo, 
    /// incluyendo su modelo, identificación, ubicación, valor monetario, mantenimiento, historial de alquiler y accesorios.
    /// </summary>
    public sealed class Vehiculo : Entity
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Vehiculo"/>.
        /// </summary>
        /// <param name="id">Identificador único del vehículo.</param>
        /// <param name="modelo">Modelo del vehículo.</param>
        /// <param name="vin">Número de identificación vehicular (VIN).</param>
        /// <param name="precio">Precio del vehículo expresado como un objeto de tipo <see cref="Moneda"/>.</param>
        /// <param name="mantenimiento">Costo estimado de mantenimiento del vehículo.</param>
        /// <param name="fechaUltimaAlquiler">Fecha en la que el vehículo fue alquilado por última vez.</param>
        /// <param name="accesorios">Lista de accesorios incluidos con el vehículo.</param>
        /// <param name="direccion">Dirección de ubicación del vehículo.</param>
        public Vehiculo(
            Guid id,
            Modelo modelo,
            Vin vin,
            Moneda precio,
            Moneda mantenimiento,
            DateTime? fechaUltimaAlquiler,
            List<Accesorio> accesorios,
            Direccion? direccion
        ) : base(id)
        {
            Modelo = modelo;
            Vin = vin;
            Precio = precio;
            Mantenimiento = mantenimiento;
            FechaUltimaAlquiler = fechaUltimaAlquiler;
            Accesorios = accesorios;
            Direccion = direccion;
        }

        /// <summary>
        /// Modelo del vehículo (e.g., "Toyota Corolla").
        /// </summary>
        public Modelo? Modelo { get; private set; }

        /// <summary>
        /// Número de identificación del vehículo (VIN - Vehicle Identification Number).
        /// </summary>
        public Vin? Vin { get; private set; }

        /// <summary>
        /// Dirección física donde está ubicado el vehículo.
        /// </summary>
        public Direccion? Direccion { get; private set; }

        /// <summary>
        /// Precio del vehículo expresado como un valor monetario.
        /// </summary>
        public Moneda? Precio { get; private set; }

        /// <summary>
        /// Costo estimado de mantenimiento, expresado como un valor monetario.
        /// </summary>
        public Moneda? Mantenimiento { get; private set; }

        /// <summary>
        /// Fecha en la que el vehículo fue alquilado por última vez.
        /// Se modifico de private a internal
        /// </summary>
        public DateTime? FechaUltimaAlquiler { get; internal set; }

        /// <summary>
        /// Lista de accesorios asociados al vehículo (e.g., GPS, asiento para niños).
        /// </summary>
        public List<Accesorio> Accesorios { get; private set; } = new();
    }
}
