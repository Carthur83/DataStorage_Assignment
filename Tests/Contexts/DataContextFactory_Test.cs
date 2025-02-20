using Data.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests.Contexts;

public static class DataContextFactory_Test
{
    //Från chat gpt, använder sqlite för att kunna testa med transaction management
    public static DataContext Create()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlite(connection)
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureCreated(); // Skapar databasen i minnet

        return context;
    }
}
