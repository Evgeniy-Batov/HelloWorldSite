using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;
using WebSite.DAL.Db.Models;

namespace WebSite.DAL.Vistadb.Models.Mapping
{
    internal class CourseMap : EntityTypeConfiguration<CourseDbM>
    {
        public CourseMap()
        {
            this.HasKey(p => p.CourseId);

            this.Property(p => p.Breadcrumb)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(p => p.Route)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(p => p.Order)
                .IsRequired();

            this.Property(p => p.PricePerMonth).IsRequired();

            this.ToTable("Course", "evgeniybatov");
            this.Property(p => p.CourseId).HasColumnName("CourseId");
            this.Property(p => p.CourseGroupId).HasColumnName("CourseGroupId");
            this.Property(p => p.Breadcrumb).HasColumnName("Breadcrumb");
            this.Property(p => p.Route).HasColumnName("Route");
            this.Property(p => p.PricePerMonth).HasColumnName("PricePerMonth");
            this.Property(p => p.CourseImageRef).HasColumnName("CourseImageRef");
            this.Property(p => p.WhatIsItHtml).HasColumnName("WhatIsItHtml");
            this.Property(p => p.WhoRequresIt).HasColumnName("WhoRequresIt");
            this.Property(p => p.WhatForHtml).HasColumnName("WhatForHtml");
            this.Property(p => p.HowToAchieve).HasColumnName("HowToAchieve");
            this.Property(p => p.WhatIsRequired).HasColumnName("WhatIsRequired");
            this.Property(p => p.Order).HasColumnName("Order");

            this.Property(p => p.TitleH1).HasColumnName("TitleH1");
            this.Property(p => p.Description).HasColumnName("Description");
            this.Property(p => p.KeyWords).HasColumnName("KeyWords");
            this.Property(p => p.Robots).HasColumnName("Robots");
            this.Property(p => p.PageTitle).HasColumnName("PageTitle");
            
            this.HasRequired(p => p.Group).
                WithMany(p => p.Courses).
                HasForeignKey(p => p.CourseGroupId);
        }
    }
}
