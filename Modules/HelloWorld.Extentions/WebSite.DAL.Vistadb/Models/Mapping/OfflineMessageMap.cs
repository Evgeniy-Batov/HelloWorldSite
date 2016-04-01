using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models.Mapping
{
    public class OfflineMessageMap:EntityTypeConfiguration<OfflineMessage>
    {
        public OfflineMessageMap()
        {
            this.HasKey(t => t.MessageId);

            this.ToTable("OfflineMessages","evgeniybatov");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn").IsRequired();
            this.Property(t => t.Email).HasColumnName("Email").IsOptional();
            this.Property(t => t.Message).HasColumnName("Message").IsRequired();
            this.Property(t => t.MessageId).HasColumnName("MessageId").IsRequired();
            this.Property(t => t.Name).HasColumnName("Name").IsRequired();
            this.Property(t => t.Phone).HasColumnName("Phone").IsOptional();
            this.Property(t => t.Topic).HasColumnName("Topic").IsOptional();
            this.Property(t => t.MessageId).HasColumnName("MessageId").IsRequired();
            this.Property(t => t.IP).HasColumnName("IP").IsOptional();
        }
    }
}
