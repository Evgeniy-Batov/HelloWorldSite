using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models.Mapping
{
    internal class CourseInfoMap: EntityTypeConfiguration<CourseInfoDbM>
    {
        public CourseInfoMap()
        {
            this.HasKey(p => p.CourseId);

            this.Property(p => p.RenderAction).IsRequired();
            this.Property(p => p.RenderFB).IsRequired();
            this.Property(p => p.RenderLength).IsRequired();
            this.Property(p => p.RenderNews).IsRequired();
            this.Property(p => p.RenderOK).IsRequired();
            this.Property(p => p.RenderPrice).IsRequired();
            this.Property(p => p.RenderSchedule).IsRequired();
            this.Property(p => p.RenderSignUp).IsRequired();
            this.Property(p => p.RenderSyllabus).IsRequired();
            this.Property(p => p.RenderVK).IsRequired();

            this.ToTable("CourseInfo", "evgeniybatov");

            this.HasRequired(p => p.Course)
                .WithOptional(c => c.CourseInfo)
                .WillCascadeOnDelete(true);
        }
    }
}
