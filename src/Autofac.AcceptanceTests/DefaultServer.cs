namespace NServiceBus.AcceptanceTests.EndpointTemplates
{
    using System;
    using System.Threading.Tasks;
    using AcceptanceTesting.Support;
    using Autofac;
    using Autofac.Core.Resolving.Pipeline;
    using Autofac.Extensions.DependencyInjection;

    public class DefaultServer : ExternallyManagedContainerServer
    {
        public override Task<EndpointConfiguration> GetConfiguration(RunDescriptor runDescriptor, EndpointCustomizationConfiguration endpointCustomizationConfiguration, Action<EndpointConfiguration> configurationBuilderCustomization) =>
            base.GetConfiguration(runDescriptor, endpointCustomizationConfiguration, endpointConfiguration =>
            {
                endpointConfiguration.UseContainer(new AutofacServiceProviderFactory(c =>
                {
                    c.ComponentRegistryBuilder.Registered += (sender, args) =>
                    {
                        args.ComponentRegistration.PipelineBuilding += (sender2, pipeline) =>
                        {
                            pipeline.Use(PipelinePhase.Activation, MiddlewareInsertionMode.EndOfPhase, (clbk, next) =>
                            {
                                next(clbk);

                                clbk.InjectProperties(clbk.Instance);
                            });
                        };
                    };
                }));

                configurationBuilderCustomization(endpointConfiguration);
            });
    }
}