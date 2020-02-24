namespace NServiceBus
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Settings for the provided dependency injection container.
    /// </summary>
    public class ContainerSettings<TContainerBuilder>
    {
        /// <summary>
        /// The <see cref="IServiceCollection"/> used to be used by the <see cref="IServiceProvider"/>.
        /// </summary>
        public IServiceCollection ServiceCollection { get; } = new ServiceCollection();

        /// <summary>
        /// Provides access to container specific configuration as part of creating the <see cref="IServiceProvider"/>.
        /// </summary>
        public void ConfigureContainer(Action<TContainerBuilder> containerConfiguration)
        {
            Guard.AgainstNull(nameof(containerConfiguration), containerConfiguration);

            ContainerConfigurations.Add(containerConfiguration);
        }

        internal List<Action<TContainerBuilder>> ContainerConfigurations { get; } = new List<Action<TContainerBuilder>>(0);

        internal ContainerSettings()
        {
        }
    }
}