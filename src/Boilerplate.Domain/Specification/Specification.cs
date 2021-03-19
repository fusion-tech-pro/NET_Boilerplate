namespace Boilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Linq.Expressions;

    #endregion

    public abstract class Specification<T> : ISpecification<T>
    {
        #region Interface Implementations

        public bool IsSatisfiedBy(T entity)
        {
            return ToExpression().Compile()(entity);
        }

        public ISpecification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public ISpecification<T> Or(Specification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        #endregion

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}