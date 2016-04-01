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
    internal class CourseGroupMap: EntityTypeConfiguration<CourseGroupDbM>
    {
        public CourseGroupMap()
        {
            this.HasKey(p => p.GroupId);

            this.Property(p => p.GroupName)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(p => p.BreadCrumb)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(p => p.RouteName)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(p => p.Order)
                .IsRequired();

            this.ToTable("CourseGroup", "evgeniybatov");
            this.Property(p => p.GroupId).HasColumnName("GroupId");
            this.Property(p => p.GroupName).HasColumnName("GroupName");
            this.Property(p => p.BreadCrumb).HasColumnName("Breadcrumb");
            this.Property(p => p.ImagePath).HasColumnName("ImagePath");
            this.Property(p => p.GoalsMarkup).HasColumnName("GoalsMarkup");
            this.Property(p => p.MethodMarkup).HasColumnName("MethodsMarkup");
            this.Property(p => p.SolutionsMarkup).HasColumnName("SolutionsMarkup");
            this.Property(p => p.RouteName).HasColumnName("RouteName");
            this.Property(p => p.Order).HasColumnName("Order");

            this.Property(p => p.TitleH1).HasColumnName("TitleH1");
            this.Property(p => p.Description).HasColumnName("Description");
            this.Property(p => p.KeyWords).HasColumnName("KeyWords");
            this.Property(p => p.Robots).HasColumnName("Robots");
            this.Property(p => p.PageTitle).HasColumnName("PageTitle");
        }

    }
}
