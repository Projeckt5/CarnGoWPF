using CarnGo.Database;
using Microsoft.EntityFrameworkCore;

namespace CarnGo
{
    public interface IDbContextFactory
    {
        IAppDbContext GetContext();
    }

    public class DbContextFactory : IDbContextFactory
    {
        public IAppDbContext GetContext()
        {
            return IoCContainer.Resolve<AppDbContext>();
        }
    }

    public class TestDbContextFactory : IDbContextFactory
    {
        public IAppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;
            return new AppDbContext(options);
        }
    }
}