using Hackney.Core.Testing.Shared;
using Xunit;

namespace AccountsApi.Tests
{
    [CollectionDefinition("LogCall collection")]
    public class LogCallAspectFixtureCollection : ICollectionFixture<LogCallAspectFixture>
    { }
}
