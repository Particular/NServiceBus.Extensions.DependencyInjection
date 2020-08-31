namespace NServiceBus
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Provides factory methods for creating endpoint instances with an externally managed container implementing the Microsoft dependency injection abstraction.
    /// </summary>
    public static class EndpointWithExternallyManagedServiceProvider
    {
        /// <summary>
        /// Creates a new startable endpoint based on the provided configuration that uses an externally managed container implementing the Microsoft dependency injection abstraction.
        /// </summary>
        /// <param name="endpointConfiguration">The endpoint configuration.</param>
        /// <param name="serviceCollection">Service collection.</param>
        public static IStartableEndpointWithExternallyManagedContainer Create(EndpointConfiguration endpointConfiguration, IServiceCollection serviceCollection)
        {
            return EndpointWithExternallyManagedContainer.Create(endpointConfiguration, serviceCollection);
        }
    }
}
