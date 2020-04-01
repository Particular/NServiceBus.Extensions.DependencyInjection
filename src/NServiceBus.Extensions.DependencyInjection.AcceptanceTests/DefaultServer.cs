namespace NServiceBus.AcceptanceTests.EndpointTemplates
{
    using System;
    using System.Threading.Tasks;
    using AcceptanceTesting.Support;
    using Microsoft.Extensions.DependencyInjection;

    public class DefaultServer : ExternallyManagedContainerServer
    {
        public override Task<EndpointConfiguration> GetConfiguration(RunDescriptor runDescriptor, EndpointCustomizationConfiguration endpointCustomizationConfiguration, Action<EndpointConfiguration> configurationBuilderCustomization)
        {
            return base.GetConfiguration(runDescriptor, endpointCustomizationConfiguration, endpointConfiguration =>
            {
                endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
                configurationBuilderCustomization(endpointConfiguration);
            });
        }
    }
}