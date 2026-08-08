public static class Extensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var store = scope.ServiceProvider.GetRequiredService<IDocumentStore>();
        await store.Storage.ApplyAllConfiguredChangesToDatabaseAsync();

        using var session = store.LightweightSession();
        await SeedAsync(session);
    }

    private static async Task SeedAsync(IDocumentSession session)
    {
        if (!await session.Query<ShoppingCart>().AnyAsync())
        {
            session.Store(InitialData.Baskets.ToArray());
            await session.SaveChangesAsync();
        }
    }
}
