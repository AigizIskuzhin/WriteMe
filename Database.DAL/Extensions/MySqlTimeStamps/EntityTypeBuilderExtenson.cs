using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.DAL.Extensions.MySqlTimeStamps
{
    internal static class EntityTypeBuilderExtension
    {
        #region UseBothTimeStampedProperties

        /// <summary>
        /// Implements Created Time (updated by the database only on INSERT) and Updated time (updated by the database only on INSERT and UPDATE)
        /// with ICreateUpdateTimeStampedEntity interface
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static EntityTypeBuilder<TEntity> UseBothTimeStampedProperties<TEntity>(
            this EntityTypeBuilder<TEntity> entity)
            where TEntity : class, ICreateUpdateTimeStampedEntity => entity
            .UseCreationTimeStampOnProperty()
            .UseUpdateTimeStampOnProperty();
        #endregion

        #region UseCreationTimeStampOnProperty
        /// <summary>
        /// Implements Created Time (updated by the database only on INSERT) with ICreationTimeStampOfEntity interface
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static EntityTypeBuilder<TEntity> UseCreationTimeStampOnProperty<TEntity>(this EntityTypeBuilder<TEntity> entity)
            where TEntity : class, ICreateTimeStampOfEntity
        {
            entity.Property(d => d.CreatedDateTime).ValueGeneratedOnAdd();

            entity.Property(d => d.CreatedDateTime).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            entity.Property(d => d.CreatedDateTime).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            return entity;
        } 
        #endregion

        #region UseUpdateTimeStampOnProperty
        /// <summary>
        /// Implements Updated time (updated by the database only on INSERT and UPDATE) with IUpdateTimeStampOfEntity interface
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static EntityTypeBuilder<TEntity> UseUpdateTimeStampOnProperty<TEntity>(this EntityTypeBuilder<TEntity> entity)
            where TEntity : class, IUpdateTimeStampOfEntity
        {
            entity.Property(d => d.UpdatedDateTime).ValueGeneratedOnAddOrUpdate();

            entity.Property(d => d.UpdatedDateTime).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            entity.Property(d => d.UpdatedDateTime).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            return entity;
        } 
        #endregion
    }
}
