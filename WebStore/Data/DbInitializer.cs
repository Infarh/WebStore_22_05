using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;

namespace WebStore.Data;

public class DbInitializer
{
    private readonly WebStoreDB _db;
    private readonly ILogger<DbInitializer> _Logger;

    public DbInitializer(WebStoreDB db, ILogger<DbInitializer> Logger)
    {
        _db = db;
        _Logger = Logger;
    }

    public async Task<bool> RemoveAsync(CancellationToken Cancel = default)
    {
        _Logger.LogInformation("Удаление БД...");
        var result = await _db.Database.EnsureDeletedAsync(Cancel).ConfigureAwait(false);
        if (result)
            _Logger.LogInformation("Удаление БД выполнено успешно");
        else
            _Logger.LogInformation("Удаление БД  не выполнено - БД отсутствовала на сервере");

        return result;
    }

    public async Task InitializeAsync(bool RemoveBefore, CancellationToken Cancel = default)
    {
        _Logger.LogInformation("Инициализация БД...");

        if (RemoveBefore)
            await RemoveAsync(Cancel).ConfigureAwait(false);

        //await _db.Database.EnsureCreatedAsync(Cancel).ConfigureAwait(false);

        _Logger.LogInformation("Применение миграций БД...");
        await _db.Database.MigrateAsync(Cancel).ConfigureAwait(false);
        _Logger.LogInformation("Применение миграций БД выполнено");

        await InitializeProductsAsync(Cancel);

        _Logger.LogInformation("Инициализация БД выполнена успешно");
    }

    private async Task InitializeProductsAsync(CancellationToken Cancel)
    {
        _Logger.LogInformation("Инициализация БД тестовыми данными...");

        if (await _db.Products.AnyAsync(Cancel).ConfigureAwait(false))
        {
            _Logger.LogInformation("Инициализация БД тестовыми данными не требуется");
            return;
        }

        await using var transaction = await _db.Database.BeginTransactionAsync(Cancel);

        _Logger.LogInformation("Добавление в БД секций...");
        await _db.Sections.AddRangeAsync(TestData.Sections, Cancel);

        await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON", Cancel);
        await _db.SaveChangesAsync(Cancel);
        await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF", Cancel);
        _Logger.LogInformation("Добавление в БД секций выполнено успешно");


        _Logger.LogInformation("Добавление в БД брендов...");
        await _db.Brands.AddRangeAsync(TestData.Brands, Cancel);

        await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON", Cancel);
        await _db.SaveChangesAsync(Cancel);
        await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF", Cancel);
        _Logger.LogInformation("Добавление в БД брендов выполнено успешно");

        _Logger.LogInformation("Добавление в БД товаров...");
        await _db.Products.AddRangeAsync(TestData.Products, Cancel);

        await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON", Cancel);
        await _db.SaveChangesAsync(Cancel);
        await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF", Cancel);
        _Logger.LogInformation("Добавление в БД товаров выполнено успешно");

        await transaction.CommitAsync(Cancel);
    }
}
