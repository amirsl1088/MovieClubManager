using MovieClubManager.Persistence.EF;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using MovieClubManager.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;

namespace MovieManagerClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;

public class BusinessIntegrationTest : EFDataContextDatabaseFixture
{
    protected EFDataContext DbContext { get; init; }
    protected EFDataContext SetupContext { get; init; }
    protected EFDataContext ReadContext { get; init; }


    protected BusinessIntegrationTest()
    {
        SetupContext = CreateDataContext();
        DbContext = CreateDataContext();
        ReadContext = CreateDataContext();
    }

    protected void Save<T>(params T[] entities)
        where T : class
    {
        foreach (var entity in entities)
        {
            DbContext.Save(entity);
        }
    }
}