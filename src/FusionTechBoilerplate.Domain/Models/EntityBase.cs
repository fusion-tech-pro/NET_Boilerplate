namespace FusionTechBoilerplate.Domain
{
    #region << Using >>

    using System;

    #endregion
    public interface IEntityBase
    {
        object Id { get; set; }
    }


    public abstract class EntityBase : IEntityBase
    {
        #region Properties

        public virtual object Id { get; set; }

        public DateTime CreateDate { get; private set; }

        public DateTime UpdateDate { get; set; }

        #endregion

        #region Constructors

        public EntityBase()
        {
            CreateDate = DateTime.UtcNow;
        }

        #endregion
    }
}