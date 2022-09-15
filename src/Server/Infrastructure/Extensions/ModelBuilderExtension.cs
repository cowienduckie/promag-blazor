using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ProMag.Server.Infrastructure.Extensions;

public static class ModelBuilderExtension
{
    public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
    {
        var applyConfigurationMethodInfo = modelBuilder
            .GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.Public)
            .First(m => m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));

        var ret = typeof(DataContext).Assembly
            .GetTypes()
            .Select(t => (t,
                i: t.GetInterfaces().FirstOrDefault(i =>
                    i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.Ordinal))))
            .Where(it => it.i != null && !it.t.IsAbstract)
            .Select(it => (et: it.i.GetGenericArguments()[0], cfgObj: Activator.CreateInstance(it.t)))
            .Select(it =>
                applyConfigurationMethodInfo.MakeGenericMethod(it.et).Invoke(modelBuilder, new[] {it.cfgObj}))
            .ToList();
    }

    public static void CascadeAllRelationsOnDelete(this ModelBuilder modelBuilder,
        DeleteBehavior behavior = DeleteBehavior.Restrict)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = behavior;
    }
}