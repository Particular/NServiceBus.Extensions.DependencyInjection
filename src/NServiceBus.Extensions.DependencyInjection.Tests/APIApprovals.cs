namespace NServiceBus.Extensions.DependencyInjection.Tests
{
    using NUnit.Framework;
    using Particular.Approvals;
    using PublicApiGenerator;

    [TestFixture]
    public class APIApprovals
    {
        [Test]
        public void Approve_NServiceBusExtensionsDependencyInjection()
        {
            var publicApi = ApiGenerator.GeneratePublicApi(typeof(ContainerExtensions).Assembly, excludeAttributes: new[] { "System.Runtime.Versioning.TargetFrameworkAttribute" });
            Approver.Verify(publicApi);
        }
    }
}