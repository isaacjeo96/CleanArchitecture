namespace CleanArchitecture.Domain.Vehiculos
{
    /// <summary>
    /// Representa los diferentes tipos de accesorios opcionales que puede tener un vehículo.
    /// </summary>
    public enum Accesorio
    {
        /// <summary>
        /// Acceso a red Wi-Fi dentro del vehículo.
        /// </summary>
        Wifi = 1,

        /// <summary>
        /// Sistema de aire acondicionado.
        /// </summary>
        AireAcondicionado = 2,

        /// <summary>
        /// Integración con Apple CarPlay.
        /// </summary>
        AppleCar = 3,

        /// <summary>
        /// Integración con Android Auto.
        /// </summary>
        AndroidCar = 4,

        /// <summary>
        /// Sistema de navegación y mapas integrado.
        /// </summary>
        Mapas = 5
    }
}
