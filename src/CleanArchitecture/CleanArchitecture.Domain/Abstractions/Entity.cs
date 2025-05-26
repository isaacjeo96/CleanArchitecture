namespace CleanArchitecture.Domain.Abstractions
{
    /// <summary>
    /// Clase base abstracta que representa una entidad dentro del dominio.
    /// Define una propiedad de identidad única y común para todas las entidades derivadas.
    /// 🧠 Explicación Funcional de la Clase Entity
    /// 📌 ¿Qué es una Entidad en DDD?
    /// En DDD, una Entidad es un objeto del dominio que tiene una identidad única que la distingue de otros objetos, incluso si sus propiedades son iguales. Su identidad no cambia durante el ciclo de vida.
    /// 🎯 Propósito de la clase Entity:
    /// Establece un contrato común para todas las entidades del dominio: todas deben tener un Id.
    /// Evita duplicación de código: al heredar de esta clase, tus entidades ya tienen implementado el manejo del Id.
    /// Promueve consistencia y reutilización.
    /// El init hace que la propiedad solo pueda establecerse en la construcción o inicialización del objeto, garantizando inmutabilidad después de creado.
    /// </summary>
    public abstract class Entity
    {

        private readonly List<IDomainEvents> _domainEvents = new();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Entity"/> con el identificador especificado.
        /// </summary>
        /// <param name="id">Identificador único de la entidad.</param>
        protected Entity(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Identificador único de la entidad.
        /// Es de solo inicialización, garantizando su inmutabilidad después de la construcción.
        /// </summary>
        public Guid Id { get; init; }
    }
}
