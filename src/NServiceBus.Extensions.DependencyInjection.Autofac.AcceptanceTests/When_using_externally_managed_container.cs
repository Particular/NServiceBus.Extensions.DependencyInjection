namespace NServiceBus.Extensions.DependencyInjection.Autofac.AcceptanceTests
{
    using System.Threading.Tasks;
    using AcceptanceTesting;
    using global::Autofac;
    using global::Autofac.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using NServiceBus.AcceptanceTests;
    using NServiceBus.AcceptanceTests.EndpointTemplates;
    using NUnit.Framework;

    public class When_using_externally_managed_container : NServiceBusAcceptanceTest
    {
        [Test]
        public async Task Should_resolve_dependencies_from_serviceprovider()
        {
            IContainer container = null;
            var context = await Scenario.Define<Context>()
                .WithEndpoint<EndpointWithExternallyManagedContainer>(endpointBuilder =>
                {
                    var serviceCollection = new ServiceCollection();
                    endpointBuilder.CustomConfig(endpointConfiguration => endpointConfiguration
                        .RegisterComponents(c => c
                            .RegisterSingleton(new InternalService())));

                    endpointBuilder.ToCreateInstance(
                        configuration => Task.FromResult(EndpointWithExternallyManagedServiceProvider.Create(configuration, serviceCollection)),
                        startableEndpoint =>
                        {
                            var containerBuilder = new ContainerBuilder();
                            containerBuilder.Populate(serviceCollection);
                            containerBuilder.RegisterInstance(new ExternalService()).SingleInstance();
                            container = containerBuilder.Build();

                            return startableEndpoint.Start(new AutofacServiceProvider(container));
                        });

                    endpointBuilder.When(session => session.SendLocal(new TestMessage()));
                })
                .Done(c => c.InjectedInternalService != null)
                .Run();

            Assert.AreSame(context.InjectedExternalService, container.Resolve<ExternalService>());
            Assert.AreSame(context.InjectedInternalService, container.Resolve<InternalService>());
        }

        class Context : ScenarioContext
        {
            public InternalService InjectedInternalService { get; set; }
            public ExternalService InjectedExternalService { get; set; }
        }

        class EndpointWithExternallyManagedContainer : EndpointConfigurationBuilder
        {
            public EndpointWithExternallyManagedContainer()
            {
                EndpointSetup<ExternallyManagedContainerServer>();
            }

            public class MessageHandler : IHandleMessages<TestMessage>
            {
                Context testContext;
                InternalService internalService;
                ExternalService externalService;

                public MessageHandler(Context testContext, InternalService internalService, ExternalService externalService)
                {
                    this.testContext = testContext;
                    this.internalService = internalService;
                    this.externalService = externalService;
                }

                public Task Handle(TestMessage message, IMessageHandlerContext context)
                {
                    testContext.InjectedInternalService = internalService;
                    testContext.InjectedExternalService = externalService;
                    return Task.CompletedTask;
                }
            }
        }

        class TestMessage : ICommand
        {
        }

        class InternalService
        {
        }

        class ExternalService
        {
            
        }
    }
}