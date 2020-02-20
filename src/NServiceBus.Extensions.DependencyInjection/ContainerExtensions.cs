namespace NServiceBus
{
    using Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using ObjectBuilder.Common;

    /// <summary>
    /// Extension methods to integrate containers support the Microsoft.Extensions.DependencyInjection model.
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Use a custom dependency injection container implementing the Microsoft.Extensions.DependencyInjection model.
        /// The container lifetime will be managed by NServiceBus.
        /// </summary>
        /// <param name="configuration">The endpoint configuration.</param>
        /// <param name="serviceProviderFactory">The <see cref="IServiceProviderFactory{TContainerBuilder}"/> of the container to be used.</param>
        public static ContainerSettings<TContainerBuilder> UseContainer<TContainerBuilder>(this EndpointConfiguration configuration,
            IServiceProviderFactory<TContainerBuilder> serviceProviderFactory)
        {
            Guard.AgainstNull(nameof(configuration), configuration);
            Guard.AgainstNull(nameof(serviceProviderFactory), serviceProviderFactory);

            var containerSettings = new ContainerSettings<TContainerBuilder>();
            IContainer containerAdapter = new ContainerAdapter<TContainerBuilder>(serviceProviderFactory, containerSettings);
            configuration.UseContainer(containerAdapter);
            return containerSettings;
        }
    }
}