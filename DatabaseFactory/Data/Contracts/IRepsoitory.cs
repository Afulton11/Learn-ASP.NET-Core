using System;
namespace DatabaseFactory.Data.Contracts
{
    public interface IRepsoitory<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Get the <typeparamref name="TEntity"/> whose primaryKey equals <paramref name="primaryKey"/>.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="primaryKey">Primary key.</param>
        TEntity Get(object primaryKey);

        /// <summary>
        /// Save the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Save(TEntity entity);

        /// <summary>
        /// Delete the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Delete(TEntity entity);
    }
}
