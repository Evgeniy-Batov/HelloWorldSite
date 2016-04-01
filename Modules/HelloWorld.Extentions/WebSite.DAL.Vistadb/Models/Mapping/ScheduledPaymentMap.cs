using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models.Models.Mapping
{
    internal class ScheduledPaymentMap: EntityTypeConfiguration<ScheduledPaymentDbM>
    {
        public ScheduledPaymentMap()
        {
            this.HasKey(s => s.PaymentId);

            this.Property(s => s.DueDate).HasColumnName("DueDate").IsRequired();

            this.Property(s => s.FlowId).HasColumnName("FlowId").IsOptional();
            this.Property(s => s.PaymentId).HasColumnName("PaymentId").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            this.Property(s => s.ScheduledAmount).HasColumnName("Amount").IsRequired();
            this.Property(s => s.Status).HasColumnName("Status").IsRequired();

            this.HasOptional(s => s.Student)
                .WithMany(s=>s.ScheduledPayments)
                .HasForeignKey(s => s.StudentId);

            this.HasOptional(s => s.Flow)
                .WithMany()
                .HasForeignKey(s => s.FlowId);


            this.Property(s => s.StudentId).HasColumnName("StudentId").IsOptional();

            this.ToTable("ScheduledPayments", "evgeniybatov");
        }
    }
}
