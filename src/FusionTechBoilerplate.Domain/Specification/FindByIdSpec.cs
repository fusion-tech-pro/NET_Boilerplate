namespace FusionTechBoilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Linq.Expressions;

    #endregion

    public class FindByIdSpec<T> : Specification<T> where T : EntityBase
    {
        #region Properties

        private readonly int id;

        #endregion

        #region Constructors

        public FindByIdSpec(int id)
        {
            this.id = id;
        }

        #endregion

        public override Expression<Func<T, bool>> ToExpression()
        {
            return r => r.Id == this.id;
        }
    }
}