using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models
{
    public class FlowMap : EntityTypeConfiguration<FlowDbModel>
    {
        public FlowMap()
        {
            this.HasKey(p => p.FlowId);

            this.Property(p => p.CourseId)
                .IsRequired();

            this.Property(p => p.Status)
                .IsRequired();

            this.ToTable("Flow","evgeniybatov");

            this.Property(p => p.FlowId).HasColumnName("FlowId");
            this.Property(p => p.CourseId).HasColumnName("CourseId");
            this.Property(p => p.EstimatedStartDate).HasColumnName("EstimatedStartDate");
            this.Property(p => p.CustomName).HasColumnName("CustomName");
            this.Property(p => p.ActualStartDate).HasColumnName("ActualStartDate");
            this.Property(p => p.Status).HasColumnName("Status");
            this.Property(p => p.ActualEndDate).HasColumnName("ActualEndDate");
            this.Property(p => p.StartDateStr).HasColumnName("StartDateStr").HasMaxLength(255).IsRequired();

            this.HasRequired(p => p.Course)
                .WithMany(p => p.Flows)
                .HasForeignKey(p => p.CourseId);
        }
    }
}
