namespace Boilerplate.Domain
{
    using System;

/*CODEREVIEW: remove access modifiers from the interface*/
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);

        public ISpecification<T> And(Specification<T> specification);

        public ISpecification<T> Or(Specification<T> specification);

        public ISpecification<T> Not();
    }
}