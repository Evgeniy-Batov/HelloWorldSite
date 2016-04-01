using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models.Models.Mapping
{
    public class ActualPaymentMap:EntityTypeConfiguration<ActualPaymentDbM>
    {
        public ActualPaymentMap()
        {
            this.HasKey(p => p.PaymentId);
            this.Property(p => p.PaymentId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            this.HasOptional(p => p.ScheduledPayment)
                .WithMany()
                .HasForeignKey(p => p.ScheduledPaymentId);

            this.HasOptional(p => p.Student)
                .WithMany()
                .HasForeignKey(p => p.StudentId);

            this.HasOptional(p => p.Flow)
                .WithMany()
                .HasForeignKey(f => f.FlowId);

            this.HasOptional(c => c.Course)
                .WithMany()
                .HasForeignKey(c => c.CourseId);

            this.ToTable("ActualPayments", "evgeniybatov");
        }
    }
}
