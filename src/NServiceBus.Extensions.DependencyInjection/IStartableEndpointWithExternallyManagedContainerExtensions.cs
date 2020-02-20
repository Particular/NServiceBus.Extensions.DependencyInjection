namespace NServiceBus
{
    using System;
    using System.Threading.Tasks;
    using Extensions.DependencyInjection;

    /// <summary>
    /// An extension point for to create an endpoint in the start-up phase where the container is externally managed using the Microsoft dependency injection abstraction.
    /// </summary>
    public static class IStartableEndpointWithExternallyManagedContainerExtensions
    {
        /// <summary>
        /// Starts the endpoint and returns a reference to it.
        /// </summary>
        /// <param name="startableEndpoint">The startable endpoint.</param>
        /// <param name="serviceProvider">The container specific service provider factory.</param>
        /// <returns>A reference to the endpoint.</returns>
        public static Task<IEndpointInstance> Start(this IStartableEndpointWithExternallyManagedContainer startableEndpoint, IServiceProvider serviceProvider)
        {
            return startableEndpoint.Start(new ServiceProviderAdapter(serviceProvider));
        }
    }
}
