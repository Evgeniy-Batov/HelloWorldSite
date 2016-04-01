using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models.Mapping
{
    public class PageLayoutMap : EntityTypeConfiguration<PageLayoutDbM>
    {
        public PageLayoutMap()
        {
            this.HasKey(p => p.Id);

            this.Property(p => p.PageName)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(p => p.PageTitle)
                .IsRequired()
                .HasMaxLength(1024);

            this.Property(p => p.TitleH1)
                .IsRequired()
                .HasMaxLength(1024);

            this.Property(p => p.Description)
                .IsRequired();

            this.ToTable("PageLayouts", "evgeniybatov");
        }
    }
}
