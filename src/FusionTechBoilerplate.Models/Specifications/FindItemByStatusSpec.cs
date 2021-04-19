using FusionTechBoilerplate.Domain;
using System;
using System.Linq.Expressions;

namespace FusionTechBoilerplate.Models
{
    public class FindItemByStatusSpec : Specification<Item>
    {
        #region Properties

        private readonly int status;

        #endregion

        #region Constructors

        public FindItemByStatusSpec(int status)
        {
            this.status = status;
        }

        #endregion

        public override Expression<Func<Item, bool>> ToExpression()
        {
            return r => r.Status == (Status)this.status;
        }
    }
}
