namespace FusionTechBoilerplate.Models
{
    #region << Using >>

    using System;
    using System.Linq.Expressions;
    using FusionTechBoilerplate.Domain;

    #endregion

    public class FindUserByIdSpec : Specification<User>
    {
        #region Properties

        private readonly string id;

        #endregion

        #region Constructors

        public FindUserByIdSpec(string id)
        {
            this.id = id;
        }

        #endregion

        public override Expression<Func<User, bool>> ToExpression()
        {
            return r => r.Id == this.id;
        }
    }
}