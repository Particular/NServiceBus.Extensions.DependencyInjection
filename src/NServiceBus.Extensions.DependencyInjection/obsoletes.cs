namespace NServiceBus
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;

    // An internal type referenced by the API approvals test as it can't reference obsoleted types.
    class InternalType
    {
    }

    /// <summary>
    /// Extension methods to integrate containers support the Microsoft.Extensions.DependencyInjection model.
    /// </summary>
    [ObsoleteEx(Message = "Use the externally managed container mode to integrate with third party dependency injection containers.",
        RemoveInVersion = "3",
        TreatAsErrorFromVersion = "2")]
    public static class ContainerExtensions
    {
        /// <summary>
        /// Use a custom dependency injection container implementing the Microsoft.Extensions.DependencyInjection model.
        /// The container lifetime will be managed by NServiceBus.
        /// </summary>
        [ObsoleteEx(Message = "Use the externally managed container mode to integrate with third party dependency injection containers.",
            RemoveInVersion = "3",
            TreatAsErrorFromVersion = "2")]
        public static ContainerSettings<TContainerBuilder> UseContainer<TContainerBuilder>(this EndpointConfiguration configuration,
            IServiceProviderFactory<TContainerBuilder> serviceProviderFactory)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Settings for the provided dependency injection container.
    /// </summary>
    [ObsoleteEx(Message = "Use the externally managed container mode to integrate with third party dependency injection containers.",
        RemoveInVersion = "3",
        TreatAsErrorFromVersion = "2")]
    public class ContainerSettings<TContainerBuilder>
    {
        /// <summary>
        /// The <see cref="IServiceCollection"/> used to be used by the <see cref="IServiceProvider"/>.
        /// </summary>
        [ObsoleteEx(Message = "Use the externally managed container mode to integrate with third party dependency injection containers.",
            RemoveInVersion = "3",
            TreatAsErrorFromVersion = "2")]
        public IServiceCollection ServiceCollection => throw new NotImplementedException();

        /// <summary>
        /// Provides access to container specific configuration as part of creating the <see cref="IServiceProvider"/>.
        /// </summary>
        [ObsoleteEx(Message = "Use the externally managed container mode to integrate with third party dependency injection containers.",
            RemoveInVersion = "3",
            TreatAsErrorFromVersion = "2")]
        public void ConfigureContainer(Action<TContainerBuilder> containerConfiguration)
        {
            throw new NotImplementedException();
        }

        internal List<Action<TContainerBuilder>> ContainerConfigurations { get; } = new List<Action<TContainerBuilder>>(0);

        internal ContainerSettings()
        {
        }
    }

    /// <summary>
    /// An extension point to create an endpoint in the start-up phase where the container is externally managed using the Microsoft dependency injection abstraction.
    /// </summary>
    [ObsoleteEx(
        ReplacementTypeOrMember = nameof(IStartableEndpointWithExternallyManagedContainer),
        RemoveInVersion = "3",
        TreatAsErrorFromVersion = "2")]
#pragma warning disable PS0024 // A non-interface type should not be prefixed with I
    public static class IStartableEndpointWithExternallyManagedContainerExtensions
#pragma warning restore PS0024 // A non-interface type should not be prefixed with I
    {
    }

    /// <summary>
    /// Provides factory methods for creating endpoint instances with an externally managed container implementing the Microsoft dependency injection abstraction.
    /// </summary>
    [ObsoleteEx(
        ReplacementTypeOrMember = nameof(EndpointWithExternallyManagedContainer),
        RemoveInVersion = "4",
        TreatAsErrorFromVersion = "3")]
    public static class EndpointWithExternallyManagedServiceProvider
    {
        /// <summary>
        /// Creates a new startable endpoint based on the provided configuration that uses an externally managed container implementing the Microsoft dependency injection abstraction.
        /// </summary>
        /// <param name="endpointConfiguration">The endpoint configuration.</param>
        /// <param name="serviceCollection">Service collection.</param>
        [ObsoleteEx(
            Message = "NServiceBus supports Microsoft.Extensions.DependencyInjection directly. Change usage of `EndpointWithExternallyManagedServiceProvider` to `EndpointWithExternallyManagedContainer` and remove the reference to the NServiceBus.Extensions.DependencyInjection package",
            RemoveInVersion = "3",
            TreatAsErrorFromVersion = "2")]
        public static IStartableEndpointWithExternallyManagedContainer Create(EndpointConfiguration endpointConfiguration, IServiceCollection serviceCollection)
        {
            throw new NotImplementedException();
        }
    }
}