namespace NServiceBus.AcceptanceTests
{
    using System;
    using System.Threading.Tasks;
    using AcceptanceTesting.Support;

    class EndpointTemplate : IEndpointSetupTemplate
    {
        public Task<EndpointConfiguration> GetConfiguration(RunDescriptor runDescriptor, EndpointCustomizationConfiguration endpointConfiguration, Action<EndpointConfiguration> configurationBuilderCustomization)
        {
            var configuration = new EndpointConfiguration(runDescriptor.ScenarioContext.TestRunId.ToString("D"));
            configuration.UseTransport<LearningTransport>();

            configuration.RegisterComponents(c => c.RegisterSingleton(runDescriptor.ScenarioContext.GetType(), runDescriptor.ScenarioContext));

            configurationBuilderCustomization(configuration);
            return Task.FromResult(configuration);
        }
    }
}