using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebSite.DAL.Db.Models.Mapping
{
    public class ChatSessionMap : EntityTypeConfiguration<ChatSession>
    {
        public ChatSessionMap()
        {
            // Primary Key
            this.HasKey(t => t.SessionId);

            // Properties
            // Table & Column Mappings
            this.ToTable("ChatSession","evgeniybatov");
            this.Property(t => t.SessionId).HasColumnName("SessionId");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.IsBotProcessing).HasColumnName("IsBotProcessing").IsRequired();
        }
    }
}
