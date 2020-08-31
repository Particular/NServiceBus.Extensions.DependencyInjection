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
            var options = new ApiGeneratorOptions
            {
                ExcludeAttributes = new[] { "System.Runtime.Versioning.TargetFrameworkAttribute" }
            };

            var publicApi = ApiGenerator.GeneratePublicApi(typeof(InternalType).Assembly, options);
            Approver.Verify(publicApi);
        }
    }
}