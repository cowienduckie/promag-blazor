using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;

namespace ProMag.Server.Infrastructure.Extensions;

public class CustomAnnotationProvider : SqlServerMigrationsAnnotationProvider
{
    public CustomAnnotationProvider(MigrationsAnnotationProviderDependencies dependencies)
        : base(dependencies)
    {
    }

    public override IEnumerable<IAnnotation> ForRemove(IColumn property)
    {
        var baseAnnotations = base.ForRemove(property);
         
        var orderAnnotation = property.FindAnnotation(CustomAnnotationNames.ColumnOrder);
            
        return orderAnnotation == null
            ? baseAnnotations
            : baseAnnotations.Concat(new[] { orderAnnotation });
    }

    public override IEnumerable<IAnnotation> ForRename(IColumn property)
    {
        var baseAnnotations = base.ForRename(property);
         
        var orderAnnotation = property.FindAnnotation(CustomAnnotationNames.ColumnOrder);
            
        return orderAnnotation == null
            ? baseAnnotations
            : baseAnnotations.Concat(new[] { orderAnnotation });
    }
}