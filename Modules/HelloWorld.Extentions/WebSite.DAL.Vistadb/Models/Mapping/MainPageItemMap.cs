using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models.Mapping
{
    public class MainPageItemMap: EntityTypeConfiguration<MainPageItemDbM>
    {
        public MainPageItemMap()
        {
            this.HasKey(p => p.Id);

            this.Property(p => p.Order).IsRequired();
            this.Property(p => p.ItemTitle).IsRequired().HasMaxLength(64);

            this.ToTable("MainPageItems", "evgeniybatov");
        }
    }
}
