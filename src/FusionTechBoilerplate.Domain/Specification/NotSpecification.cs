namespace Boilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Linq;
    using System.Linq.Expressions;

    #endregion

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