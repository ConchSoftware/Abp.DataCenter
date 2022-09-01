using Abp.DataCenter.Excel;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.DataCenter.EntityFrameworkCore;

public static class DataCenterDbContextModelCreatingExtensions
{
    public static void ConfigureDataCenter(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<ExcelUploadConfigMaster>(b =>
        {
            b.ConfigureByConvention();

            b.Property(q => q.SheetName).IsRequired().HasMaxLength(64);
            b.Property(q => q.ConfigName).IsRequired().HasMaxLength(64);
            b.HasMany(q => q.ExcelUploadConfigItems).WithOne().HasForeignKey(qt => qt.ConfigId);
        });

        builder.Entity<ExcelUploadConfigItem>(b =>
        {
            b.ConfigureByConvention();

            b.Property(q => q.IsRequired).HasDefaultValue(false);
            b.Property(q => q.ColumnName).IsRequired().HasMaxLength(64);
            b.Property(q => q.DefaultValue).IsRequired(false).HasMaxLength(128);
        });

        builder.Entity<ExcelExportConfigMaster>(b =>
        {
            b.ConfigureByConvention();

            b.Property(q => q.SheetName).IsRequired().HasMaxLength(64);
            b.Property(q => q.FileName).IsRequired().HasMaxLength(64);
            b.HasMany(q => q.ExcelExportConfigItems).WithOne().HasForeignKey(qt => qt.ConfigId);
        });

        builder.Entity<ExcelExportConfigItem>(b =>
        {
            b.ConfigureByConvention();

            b.Property(q => q.FieldName).IsRequired().HasMaxLength(64);
            b.Property(q => q.ColumnName).IsRequired(false).HasMaxLength(64);
        });
    }
}
