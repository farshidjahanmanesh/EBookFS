using EBookFS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookFS.tests.UnitTests.Helpers
{
    public static class ContextFactory
    {
        public static BookDBContext DBContextFactoryHelper()
        {
            var options = new DbContextOptionsBuilder<BookDBContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var context = new BookDBContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
