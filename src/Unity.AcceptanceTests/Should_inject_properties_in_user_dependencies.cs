namespace NServiceBus.AcceptanceTests
{
    using System.Threading.Tasks;
    using AcceptanceTesting;
    using NUnit.Framework;
    using Unity;
    using Unity.Microsoft.DependencyInjection;

    [TestFixture]
    public class Should_inject_properties_in_user_dependencies
    {
        [Test]
        public async Task Should_support_property_injection()
        {
            await Scenario.Define<TestContext>()
                .WithEndpoint<TestEndpoint>(e => e
                    .When(s => s.SendLocal(new TestMessage())))
                .Done(c => c.PropertyInjected)
                .Run();
        }

        class TestContext : ScenarioContext
        {
            public bool PropertyInjected { get; set; }
        }

        class TestEndpoint : EndpointConfigurationBuilder
        {
            public TestEndpoint()
            {
                EndpointSetup<EndpointTemplate>(configuration =>
                {
                    configuration.UseContainer<IUnityContainer>(new ServiceProviderFactory(null));
                });
            }

            class TestHandler : IHandleMessages<TestMessage>
            {
                [Dependency] // property injection requires attributes defined by the user
                public TestContext TestContext { get; set; }

                public Task Handle(TestMessage message, IMessageHandlerContext context)
                {
                    TestContext.PropertyInjected = TestContext != null;
                    return Task.CompletedTask;
                }
            }
        }

        class TestMessage : IMessage
        {
        }
    }
}