namespace Boilerplate.Domain
{
    #region << Using >>

    using System;
    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public class InjectableAttribute : Attribute
    {
        #region Properties

        public ServiceLifetime Lifetime { get; }

        #endregion

        #region Constructors

        public InjectableAttribute(ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            Lifetime = lifetime;
        }

        #endregion
    }
}