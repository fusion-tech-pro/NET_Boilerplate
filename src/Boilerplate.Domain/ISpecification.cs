namespace Boilerplate.Domain
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);

        ISpecification<T> And(Specification<T> specification);

        ISpecification<T> Or(Specification<T> specification);

        ISpecification<T> Not();
    }
}