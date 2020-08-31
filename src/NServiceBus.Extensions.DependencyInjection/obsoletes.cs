namespace NServiceBus
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;

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
}