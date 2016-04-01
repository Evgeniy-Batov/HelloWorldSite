using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models.Models.Mapping
{
    public class EmailMap : EntityTypeConfiguration<EmailDbModel>
    {
        public EmailMap()
        {
            this.HasKey(p => p.EmailId);

            this.ToTable("Email", "evgeniybatov");

            this.Property(p => p.EmailId).IsRequired().HasColumnName("EmailId");
            this.Property(p => p.From).IsRequired().HasMaxLength(64).HasColumnName("From");
            this.Property(p => p.Subject).IsRequired().HasMaxLength(255).HasColumnName("Subject");
            this.Property(p => p.Body).IsRequired().HasColumnName("Body");
            this.Property(p => p.SentTime).IsOptional().HasColumnName("SentTime");
            this.Property(p => p.Status).IsRequired().HasColumnName("Status");
            this.Property(p => p.IsHtml).IsRequired().HasColumnName("IsHtml");
        }
    }

    public class EmailTemplateMap : EntityTypeConfiguration<EmailTemplateDbModel>
    {
        public EmailTemplateMap()
        {
            this.HasKey(p => p.TemplateID);

            this.ToTable("EmailTemplate", "evgeniybatov");

            this.Property(p => p.TemplateID).IsRequired().HasColumnName("TemplateID");
            this.Property(p => p.HtmlVersion).IsOptional().HasColumnName("HtmlVersion");
            this.Property(p => p.TextVersion).IsOptional().HasColumnName("TextVerstion");
        }

    }

    public class EmailRecipientMap : EntityTypeConfiguration<EmailRecipientDbModel>
    {
        public EmailRecipientMap()
        {
            this.HasKey(p => p.RecepientId);

            this.ToTable("EmailRecipient", "evgeniybatov");

            this.Property(p => p.RecepientId).IsRequired().HasColumnName("RecipientId");
            this.Property(p => p.EmailId).IsRequired().HasColumnName("EmailId");
            this.Property(p => p.Recepient).IsRequired().HasMaxLength(128).HasColumnName("Recipient");
            this.Property(p => p.To).IsRequired().HasColumnName("To");
            this.Property(p => p.CC).IsRequired().HasColumnName("CC");
            this.Property(p => p.BCC).IsRequired().HasColumnName("BCC");

            // Relationships
            this.HasRequired(p => p.Email)
                .WithMany(e => e.Recipients)
                .HasForeignKey(p => p.EmailId);
        }
    }
}
