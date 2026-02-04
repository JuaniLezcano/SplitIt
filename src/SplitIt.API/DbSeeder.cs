using Microsoft.EntityFrameworkCore;
using SplitIt.Domain.Entities;
using SplitIt.Persistence;

namespace SplitIt.API;

public static class DataSeeder
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SplitItDbContext>();

        // Asumimos que en Program.cs ya corriste MigrateAsync() (solo en Development).
        await SeedAsync(context);
    }

    private static async Task SeedAsync(SplitItDbContext context)
    {
        // Si ya hay relaciones UserGroup, consideramos que ya está seed-eado lo importante
        if (await context.UserGroups.AnyAsync())
            return;

        // ---------- Users ----------
        var juani = new User
        {
            Name = "Juan Ignacio Lezcano",
            Email = "juanilezca03@hotmail.com",
            Password = "juani123" // SOLO DEV
        };

        var mario = new User
        {
            Name = "Felix Mario Lezcano",
            Email = "mariolezcano_mv@gmail.com",
            Password = "mario123" // SOLO DEV
        };

        // ---------- Group ----------
        var family = new Group
        {
            Name = "Family",
            Description = "Family expenses group"
        };

        // ---------- UserGroups ----------
        var ugJuani = new UserGroup
        {
            User = juani,
            Group = family,
            IsAdmin = true,
            IsInvitationAccepted = true
        };

        var ugMario = new UserGroup
        {
            User = mario,
            Group = family,
            IsAdmin = false,
            IsInvitationAccepted = true
        };

        // ---------- Expense ----------
        var expense = new Expense
        {
            Description = "Cena",
            Group = family,
            CreatedByUserGroup = ugJuani,
            Amount = 10000m,
            Type = SplitIt.Domain.Enums.ExpenseType.Equal
        };

        // ---------- ExpenseSplits ----------
        var splitJuani = new ExpenseSplit
        {
            Expense = expense,
            UserGroup = ugJuani,
            OwedAmount = 5000m
        };

        var splitMario = new ExpenseSplit
        {
            Expense = expense,
            UserGroup = ugMario,
            OwedAmount = 5000m
        };

        // ---------- Payment (opcional, pero útil) ----------
        var payment = new Payment
        {
            Expense = expense,
            UserGroup = ugMario,
            Amount = 5000m
        };

        await context.Users.AddRangeAsync(juani, mario);
        await context.Groups.AddAsync(family);
        await context.UserGroups.AddRangeAsync(ugJuani, ugMario);
        await context.Expenses.AddAsync(expense);
        await context.ExpenseSplits.AddRangeAsync(splitJuani, splitMario);
        await context.Payments.AddAsync(payment);

        await context.SaveChangesAsync();
    }
}
