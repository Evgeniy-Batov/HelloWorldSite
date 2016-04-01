using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models.Models.Mapping
{
    internal class StudentMap: EntityTypeConfiguration<StudentDbM>
    {
        public StudentMap()
        {
            this.HasKey(s => s.StudentId);

            this.Property(s => s.ContractNumber).HasColumnName("ContractNo").HasMaxLength(64);
            this.Property(s => s.Email).HasColumnName("Email").IsRequired();
            this.Property(s => s.FirstName).HasColumnName("FirstName").HasMaxLength(64);
            this.Property(s => s.FlowId).HasColumnName("FlowId");

            this.HasRequired(p => p.Flow).
                WithMany().
                HasForeignKey(p => p.FlowId);

            this.Property(s => s.INN).HasColumnName("Inn").HasMaxLength(128);
            this.Property(s => s.LastName).HasColumnName("LastName").HasMaxLength(128);
            this.Property(s=>s.MiddleName).HasColumnName("MiddleName").HasMaxLength(128);
            this.Property(s => s.PasspordIssuedDate).HasColumnName("PasspordIssuedDate").IsOptional();
            this.Property(s => s.PassportIssuedAt).HasColumnName("PassportIssuedAt").IsOptional().HasMaxLength(512);
            this.Property(s => s.PassportIssuedBy).HasColumnName("PassportIssuedBy").IsOptional().HasMaxLength(512);
            this.Property(s => s.PassportNo).HasColumnName("PassportNo").IsOptional().HasMaxLength(64);

            this.Property(s => s.Phone1).HasColumnName("Phone1").IsOptional().HasMaxLength(64);
            this.Property(s => s.Phone2).HasColumnName("Phone2").IsOptional().HasMaxLength(64);

            this.Property(s => s.StudentId).HasColumnName("StudentId").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            this.Property(s => s.StudentSince).HasColumnName("StudentSince").IsRequired();
            this.Property(s => s.StudentTill).HasColumnName("StudentTill").IsOptional();
            this.Property(s => s.PaymentModel).HasColumnName("PaymentModel").IsRequired();
            this.Property(s => s.Comment).HasColumnName("Comment").IsOptional();

            this.ToTable("Student", "evgeniybatov");
        }
    }
}
