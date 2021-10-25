namespace NServiceBus.Extensions.DependencyInjection.AcceptanceTests
{
    using System;
    using System.Threading.Tasks;
    using AcceptanceTesting;
    using NServiceBus.AcceptanceTests;
    using NServiceBus.AcceptanceTests.EndpointTemplates;
    using NUnit.Framework;

    // Castle.Windsor has special mechanisms to track disposable instances which can cause issues with the
    // registration of the implemented interfaces of the disposable type.
    public class When_injecting_disposable_service : NServiceBusAcceptanceTest
    {
        [Test]
        public async Task Should_not_throw_on_disposable_services()
        {
            await Scenario.Define<Context>()
                .WithEndpoint<EndpointWithDisposableService>(endpointBuilder => endpointBuilder
                    .When(session => session.SendLocal(new DemoMessage())))
                .Done(c => c.MessageHandled)
                .Run();
        }

        public class Context : ScenarioContext
        {
            public bool MessageHandled { get; set; }
        }

        public class EndpointWithDisposableService : EndpointConfigurationBuilder
        {
            public EndpointWithDisposableService()
            {
                EndpointSetup<DefaultServer>(c =>
                {
                    c.RegisterComponents(r => r
                        .ConfigureComponent<MyService>(DependencyLifecycle.SingleInstance));
                });
            }

            public class DemoMessageHandler : IHandleMessages<DemoMessage>
            {
                Context testContext;
#pragma warning disable IDE0052 // Remove unread private members
                IMyService dependency;
                IMyOtherService anotherDependency;
#pragma warning restore IDE0052 // Remove unread private members

                public DemoMessageHandler(Context testContext, IMyService dependency, IMyOtherService anotherDependency)
                {
                    this.testContext = testContext;
                    this.dependency = dependency;
                    this.anotherDependency = anotherDependency;
                }

                public Task Handle(DemoMessage message, IMessageHandlerContext context)
                {
                    testContext.MessageHandled = true;
                    return Task.FromResult(0);
                }
            }
        }

        public interface IMyService
        {
        }

        public interface IMyOtherService
        {
        }

        public class MyService : IMyService, IMyOtherService, IDisposable
        {
            public void Dispose()
            {
            }
        }

        public class DemoMessage : IMessage
        {
        }
    }
}