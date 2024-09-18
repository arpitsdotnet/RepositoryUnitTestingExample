using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration.TestContext;
public static class InMemoryContextGenerator
{
    public static ApplicationDbContext Generate()
    {
        var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

        return new ApplicationDbContext(options: optionBuilder.Options);
    }
}
