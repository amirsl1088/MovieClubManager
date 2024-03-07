using MovieClubManager.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Xunit;

namespace MovieClubMamager.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;

[CollectionDefinition(nameof(ConfigurationFixture), DisableParallelization = false)]
public class ConfigurationCollectionFixture : ICollectionFixture<ConfigurationFixture>
{
}