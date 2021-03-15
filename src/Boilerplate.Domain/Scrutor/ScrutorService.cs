namespace Boilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Scrutor;

    #endregion

    public class ScrutorService
    {
        #region Nested Classes

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

        [Injectable(ServiceLifetime.Transient)]
        public class TransientInjectableService : ScrutorIService.ITransientService
        {
            #region Properties

            private readonly string guid = Guid.NewGuid().ToString();

            #endregion

            #region Interface Implementations

            public string GetValue()
            {
                return this.guid + " by Injectable";
            }

            #endregion
        }

        [Injectable(ServiceLifetime.Scoped)]
        public class ScopedInjectableService : ScrutorIService.IScopedService
        {
            #region Properties

            private readonly string guid = Guid.NewGuid().ToString();

            #endregion

            #region Interface Implementations

            public string GetValue()
            {
                return this.guid + " by Injectable";
            }

            #endregion
        }

        [Injectable(ServiceLifetime.Singleton)]
        public class SingletonInjectableService : ScrutorIService.ISingletonService
        {
            #region Properties

            private readonly string guid = Guid.NewGuid().ToString();

            #endregion

            #region Interface Implementations

            public string GetValue()
            {
                return this.guid + " by Injectable";
            }

            #endregion
        }

        #endregion
    }

    public static class ScrutorExtensions
    {
        public static IImplementationTypeSelector InjectableAttributes(this IImplementationTypeSelector selector)
        {
            var lifeTimes = Enum.GetValues(typeof(ServiceLifetime)).Cast<ServiceLifetime>();

            foreach (var item in lifeTimes)
                selector = selector.InjectableAttribute(item);

            return selector;
        }

        public static IImplementationTypeSelector InjectableAttribute(this IImplementationTypeSelector selector, ServiceLifetime lifeTime)
        {
            return selector.AddClasses(c => c.WithAttribute<ScrutorService.InjectableAttribute>(i => i.Lifetime == lifeTime))
                           .AsImplementedInterfaces()
                           .WithLifetime(lifeTime);
        }
    }
}