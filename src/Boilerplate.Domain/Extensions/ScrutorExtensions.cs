namespace Boilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Scrutor;

    #endregion

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
            return selector.AddClasses(c => c.WithAttribute<InjectableAttribute>(i => i.Lifetime == lifeTime))
                           .AsImplementedInterfaces()
                           .WithLifetime(lifeTime);
        }
    }
}