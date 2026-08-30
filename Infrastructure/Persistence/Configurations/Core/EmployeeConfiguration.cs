using CarRental.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Core
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees", "core");

            builder.HasIndex(e => new { e.BranchId, e.IsActive }, "IX_Employees_BranchId_IsActive");

            builder.HasIndex(e => e.EmployeeNumber, "UQ_Employees_EmployeeNumber").IsUnique();

            builder.HasIndex(e => e.UserId, "UQ_Employees_UserId").IsUnique();

            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.EmployeeNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.JobTitle).HasMaxLength(100);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);

            builder.HasOne(d => d.Branch).WithMany(p => p.Employees)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Employees_Branches");

            builder.HasOne(d => d.User).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Employees_Users");
        }
    }
}
