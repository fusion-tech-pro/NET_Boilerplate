namespace FusionTechBoilerplate.Models
{
    #region << Using >>

    using System;
    using System.Linq.Expressions;
    using FusionTechBoilerplate.Domain;

    #endregion

    public interface IUserProperty
    {
        User User {get;set;}
    }

    public class FindItemByUserIdSpec<T> : Specification<T> where T : IUserProperty
    {
        #region Properties

        private readonly string userId;

        #endregion

        #region Constructors

        public FindItemByUserIdSpec(string userId)
        {
            this.userId = userId;
        }

        #endregion

        public override Expression<Func<T, bool>> ToExpression()
        {
            return r => r.User.Id == this.userId;
        }
    }
}