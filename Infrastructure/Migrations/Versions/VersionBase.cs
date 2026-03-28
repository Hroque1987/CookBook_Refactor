using System;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Infrastructure.Migrations.Versions;

public abstract class VersionBase : ForwardOnlyMigration // Only need to implement UP. If Inherits Migration has Up AND Down
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
    {
       return Create.Table(table)
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Active").AsBoolean().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable();
    }
}
