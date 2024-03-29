﻿namespace FusionTechBoilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Linq.Expressions;

    #endregion

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

            return ExpressionExtensions.And<T>(leftExpression, rightExpression);
        }
    }
}