using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebSite.DAL.Db.Models.Mapping
{
    public class ChatSessionMessageMap : EntityTypeConfiguration<ChatSessionMessage>
    {
        public ChatSessionMessageMap()
        {
            // Primary Key
            this.HasKey(t => t.MessageId);

            // Properties
            this.Property(t => t.Author)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.MessageText)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ChatSessionMessages","evgeniybatov");
            this.Property(t => t.MessageId).HasColumnName("MessageId");
            this.Property(t => t.SessionId).HasColumnName("SessionId");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.PostedOn).HasColumnName("PostedOn");
            this.Property(t => t.MessageText).HasColumnName("MessageText");
            this.Property(t => t.MessageType).HasColumnName("MessageType").IsOptional();

            // Relationships
            this.HasRequired(t => t.ChatSession)
                .WithMany(t => t.ChatSessionMessages)
                .HasForeignKey(d => d.SessionId);

        }
    }
}
