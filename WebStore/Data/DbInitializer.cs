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

    public async Task InitializeAsync(
        bool RemoveBefore,
        bool AddTestData,
        CancellationToken Cancel = default)
    {
        _Logger.LogInformation("Инициализация БД...");

        if (RemoveBefore)
            await RemoveAsync(Cancel).ConfigureAwait(false);

        //await _db.Database.EnsureCreatedAsync(Cancel).ConfigureAwait(false);

        _Logger.LogInformation("Применение миграций БД...");
        await _db.Database.MigrateAsync(Cancel).ConfigureAwait(false);
        _Logger.LogInformation("Применение миграций БД выполнено");

        if (AddTestData)
        {
            await InitializeProductsAsync(Cancel);
            await InitializeEmployeesAsync(Cancel);
        }

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

        var sections_pool = TestData.Sections.ToDictionary(s => s.Id);
        var brands_pool = TestData.Brands.ToDictionary(b => b.Id);

        foreach (var child_section in TestData.Sections.Where(s => s.ParentId is not null))
            child_section.Parent = sections_pool[child_section.Id];

        foreach (var product in TestData.Products)
        {
            product.Section = sections_pool[product.SectionId];
            if (product.BrandId is { } brand_id)
                product.Brand = brands_pool[brand_id];

            product.Id = 0;
            product.SectionId = 0;
            product.BrandId = null;
        }

        foreach (var brand in TestData.Brands)
            brand.Id = 0;

        foreach (var section in TestData.Sections)
        {
            section.Id = 0;
            section.ParentId = null;
        }

        await using var transaction = await _db.Database.BeginTransactionAsync(Cancel);

        _Logger.LogInformation("Добавление данных в БД...");
        await _db.Sections.AddRangeAsync(TestData.Sections, Cancel);
        await _db.Brands.AddRangeAsync(TestData.Brands, Cancel);
        await _db.Products.AddRangeAsync(TestData.Products, Cancel);

        await _db.SaveChangesAsync(Cancel);
        _Logger.LogInformation("Добавление данных в БД выполнено успешно");

        await transaction.CommitAsync(Cancel);
        _Logger.LogInformation("Транзакция в БД завершена");
    }

    private async Task InitializeEmployeesAsync(CancellationToken Cancel)
    {
        if (await _db.Employees.AnyAsync(Cancel).ConfigureAwait(false))
        {
            _Logger.LogInformation("Инициализация таблицы сотрудников в БД не требуется");
            return;
        }

        _Logger.LogInformation("Инициализация БД сотрудников...");

        foreach (var employee in TestData.Employees)
            employee.Id = 0;

        await _db.AddRangeAsync(TestData.Employees, Cancel);
        await _db.SaveChangesAsync(Cancel);

        _Logger.LogInformation("Инициализация БД сотрудников выполнена успешно");

    }
}
