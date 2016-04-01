using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models.Models.Mapping
{
    public class CoursePriceMap : EntityTypeConfiguration<CoursePriceDbM>
    {
        public CoursePriceMap()
        {
            this.HasKey(e => e.PriceId);
            this.ToTable("CourseAdditionalPrice","evgeniybatov");

            this.Property(e => e.PriceId).HasColumnName("PriceId").IsRequired();
            this.Property(e => e.CourseId).HasColumnName("CourseId").IsRequired();
            this.Property(e => e.Price).HasColumnName("Price").IsRequired();
            this.Property(e => e.Conditional).HasColumnName("Conditional").IsOptional();
            this.Property(e => e.CustomCSS).HasColumnName("CustomCSS").IsOptional();
            this.Property(e => e.ValidTill).HasColumnName("ValidTill").IsOptional();
            this.Property(e => e.ShortConditional).HasColumnName("ShortConditional").IsOptional();

            this.HasRequired(e => e.Course)
                .WithMany(c => c.AdditionalPrices)
                .HasForeignKey(e => e.CourseId);
        }
    }   
}
