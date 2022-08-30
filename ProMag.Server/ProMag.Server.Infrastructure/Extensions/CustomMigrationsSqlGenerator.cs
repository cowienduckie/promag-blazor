﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace ProMag.Server.Infrastructure.Extensions;

public class CustomMigrationsSqlGenerator : SqlServerMigrationsSqlGenerator
{
    public CustomMigrationsSqlGenerator(
        MigrationsSqlGeneratorDependencies dependencies,
        IRelationalAnnotationProvider migrationsAnnotations)
        : base(dependencies, migrationsAnnotations)
    {
    }

    protected override void CreateTableColumns(CreateTableOperation operation, IModel model,
        MigrationCommandListBuilder builder)
    {
        operation.Columns.Sort(new ColumnOrderComparision());
        base.CreateTableColumns(operation, model, builder);
    }

    internal class ColumnOrderComparision : IComparer<AddColumnOperation>
    {
        public int Compare(AddColumnOperation x, AddColumnOperation y)
        {
            var orderX = Convert.ToInt32(x.FindAnnotation(CustomAnnotationNames.ColumnOrder)?.Value ?? 0);
            var orderY = Convert.ToInt32(y.FindAnnotation(CustomAnnotationNames.ColumnOrder)?.Value ?? 0);
            return orderX.CompareTo(orderY);
        }
    }
}