namespace Boilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Linq;
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
    
    public class AndSpecification<T> : Specification<T>
    {
        #region Properties

        private readonly Specification<T> _left;

        private readonly Specification<T> _right;

        #endregion

        #region Constructors

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            this._left = left ?? throw new ArgumentNullException(nameof(left));
            this._right = right ?? throw new ArgumentNullException(nameof(right));
        }

        #endregion

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = this._left.ToExpression();
            var rightExpression = this._right.ToExpression();

            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }

    public class OrSpecification<T> : Specification<T>
    {
        #region Properties

        private readonly Specification<T> _left;

        private readonly Specification<T> _right;

        #endregion

        #region Constructors

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            this._left = left ?? throw new ArgumentNullException(nameof(left));
            this._right = right ?? throw new ArgumentNullException(nameof(right));
        }

        #endregion

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = this._left.ToExpression();
            var rightExpression = this._right.ToExpression();

            var orExpression = Expression.OrAssign(leftExpression.Body, rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
        }
    }

    public class NotSpecification<T> : Specification<T>
    {
        #region Properties

        private readonly Specification<T> _specification;

        #endregion

        #region Constructors

        public NotSpecification(Specification<T> specification)
        {
            this._specification = specification ?? throw new ArgumentNullException(nameof(specification));
        }

        #endregion

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression = this._specification.ToExpression();

            var notExpression = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }
}