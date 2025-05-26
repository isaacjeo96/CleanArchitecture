using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users
{
    /// <summary>
    /// Representa un usuario dentro del dominio del sistema.
    /// Esta entidad contiene información personal como nombre, apellido y correo electrónico.
    /// </summary>
    public sealed class User : Entity
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="User"/> con los datos especificados.
        /// Este constructor es privado para forzar el uso del método de fábrica <see cref="Create"/>.
        /// </summary>
        /// <param name="id">Identificador único del usuario.</param>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="apellido">Apellido del usuario.</param>
        /// <param name="email">Correo electrónico del usuario.</param>
        private User(Guid id, Nombre nombre, Apellido apellido, Email email)
            : base(id)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
        }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public Nombre? Nombre { get; private set; }

        /// <summary>
        /// Apellido del usuario.
        /// </summary>
        public Apellido? Apellido { get; private set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public Email? Email { get; private set; }

        /// <summary>
        /// Crea una nueva instancia de <see cref="User"/> asignando un identificador único.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="apellido">Apellido del usuario.</param>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <returns>Una instancia nueva de la entidad <see cref="User"/>.</returns>
        public static User Create(Nombre nombre, Apellido apellido, Email email)
        {
            var user = new User(Guid.NewGuid(), nombre, apellido, email);
            return user;
        }
    }
}
