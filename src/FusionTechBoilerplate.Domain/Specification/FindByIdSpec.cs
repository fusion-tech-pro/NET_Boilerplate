﻿namespace FusionTechBoilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Linq.Expressions;

    #endregion

    public class FindByIdSpec<T> : Specification<T> where T : IEntityBase
    {
        #region Properties

        private readonly object id;

        #endregion

        #region Constructors

        public FindByIdSpec(object id)
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