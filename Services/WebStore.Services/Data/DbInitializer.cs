using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Data;

public class DbInitializer
{
    private readonly WebStoreDB _db;
    private readonly UserManager<User> _UserManager;
    private readonly RoleManager<Role> _RoleManager;
    private readonly ILogger<DbInitializer> _Logger;

    public DbInitializer(
        WebStoreDB db,
        UserManager<User> UserManager,
        RoleManager<Role> RoleManager,
        ILogger<DbInitializer> Logger)
    {
        _db = db;
        _UserManager = UserManager;
        _RoleManager = RoleManager;
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

        await InitializeIdentityAsync(Cancel);

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
            child_section.Parent = sections_pool[(int)child_section.ParentId!]; // тут была ошибка!

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
        await _db.SaveChangesAsync(Cancel);

        await _db.Brands.AddRangeAsync(TestData.Brands, Cancel);
        await _db.SaveChangesAsync(Cancel);

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

    private async Task InitializeIdentityAsync(CancellationToken Cancel)
    {
        _Logger.LogInformation("Инициализация БД системы Identity...");

        async Task CheckRoleAsync(string RoleName)
        {
            if (await _RoleManager.RoleExistsAsync(RoleName))
                _Logger.LogInformation("Роль {0} существует в БД", RoleName);
            else
            {
                _Logger.LogInformation("Роль {0} отсутствует в БД. Создаю...", RoleName);
                await _RoleManager.CreateAsync(new Role { Name = RoleName });
                _Logger.LogInformation("Роль {0} успешно создана", RoleName);
            }
        }

        await CheckRoleAsync(Role.Administrators);
        await CheckRoleAsync(Role.Users);

        if (await _UserManager.FindByNameAsync(User.Administrator) is null)
        {
            _Logger.LogInformation("Пользователь {0} отсутствует в БД. Создаю...", User.Administrator);

            var admin = new User
            {
                UserName = User.Administrator
            };

            var creation_result = await _UserManager.CreateAsync(admin, User.AdminPassword);
            if (creation_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} создан. Наделяю его ролью администратора.", User.Administrator);

                await _UserManager.AddToRoleAsync(admin, Role.Administrators);

                _Logger.LogInformation("Пользователь {0} наделён ролью администратора.", User.Administrator);
            }
            else
            {
                var errors = creation_result.Errors.Select(e => e.Description);
                var error_message = string.Join(", ", errors);
                _Logger.LogError("Учётная запись {0} не может быть создана. Ошибка: {1}",
                    User.Administrator,
                    error_message);

                throw new InvalidOperationException($"Невозможнос создать {User.Administrator}. Ошибка: {error_message}");
            }
        }
        else
            _Logger.LogInformation("Пользователь {0} существует", User.Administrator);

        _Logger.LogInformation("Инициализация БД системы Identity выполнена успешно");
    }
}
