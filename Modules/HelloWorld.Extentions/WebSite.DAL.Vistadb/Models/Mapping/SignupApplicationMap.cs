using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models
{
    class SignupApplicationMap : EntityTypeConfiguration<SignupApplicationDbModel>
    {
        public SignupApplicationMap()
        {
            this.HasKey(p => p.ApplicationId);

            this.ToTable("SignupApplication", "evgeniybatov");

            this.Property(p => p.ApplicationId).HasColumnName("Applicationid").IsRequired();
            this.Property(p => p.FlowId).HasColumnName("FlowId").IsRequired();
            this.Property(p => p.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(64);
            this.Property(p => p.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(64);
            this.Property(p => p.Email).HasColumnName("Email").IsRequired().HasMaxLength(64);
            this.Property(p => p.Phone).HasColumnName("Phone").IsRequired().HasMaxLength(64);
            this.Property(p => p.Refferal).HasColumnName("Refferal").IsRequired().HasMaxLength(64);
            this.Property(p => p.MondayTime).HasColumnName("MondayTime").IsRequired();
            this.Property(p => p.TuesdayTime).HasColumnName("TuesdayTime").IsRequired();
            this.Property(p => p.WednesdayTime).HasColumnName("WednesdayTime").IsRequired();
            this.Property(p => p.ThursdayTime).HasColumnName("ThursdayTime").IsRequired();
            this.Property(p => p.FridayTime).HasColumnName("FridayTime").IsRequired();
            this.Property(p => p.SaturdayTime).HasColumnName("SaturdayTime").IsRequired();
            this.Property(p => p.SundayTime).HasColumnName("SundayTime").IsRequired();
            this.Property(p => p.AdditionalInfo).HasColumnName("AdditionalInfo").IsOptional();
            this.Property(p => p.CreatedOn).HasColumnName("CreatedOn").IsRequired();
            this.Property(p => p.IP).HasColumnName("IP").IsOptional();
            this.Property(p => p.Status).HasColumnName("Status").IsRequired();
            this.Property(p => p.AccessToken).HasColumnName("AccessToken").IsRequired();

            this.HasRequired(p => p.Flow)
                .WithMany(f => f.Applications)
                .HasForeignKey(p => p.FlowId);
        }
    }

    class CanceledSignupApplicationMap : EntityTypeConfiguration<CanceledSignupApplicationDbModel>
    {
        public CanceledSignupApplicationMap()
        {
            this.HasKey(p => p.ApplicationId);

            this.ToTable("CanceledSignupApplication", "evgeniybatov");

            this.Property(p => p.ApplicationId).HasColumnName("Applicationid").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.FlowId).HasColumnName("FlowId").IsRequired();
            this.Property(p => p.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(64);
            this.Property(p => p.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(64);
            this.Property(p => p.Email).HasColumnName("Email").IsRequired().HasMaxLength(64);
            this.Property(p => p.Phone).HasColumnName("Phone").IsRequired().HasMaxLength(64);
            this.Property(p => p.Refferal).HasColumnName("Refferal").IsRequired().HasMaxLength(64);
            this.Property(p => p.MondayTime).HasColumnName("MondayTime").IsRequired();
            this.Property(p => p.TuesdayTime).HasColumnName("TuesdayTime").IsRequired();
            this.Property(p => p.WednesdayTime).HasColumnName("WednesdayTime").IsRequired();
            this.Property(p => p.ThursdayTime).HasColumnName("ThursdayTime").IsRequired();
            this.Property(p => p.FridayTime).HasColumnName("FridayTime").IsRequired();
            this.Property(p => p.SaturdayTime).HasColumnName("SaturdayTime").IsRequired();
            this.Property(p => p.SundayTime).HasColumnName("SundayTime").IsRequired();
            this.Property(p => p.AdditionalInfo).HasColumnName("AdditionalInfo").IsOptional();
            this.Property(p => p.CreatedOn).HasColumnName("CreatedOn").IsRequired();
            this.Property(p => p.IP).HasColumnName("IP").IsOptional();
            this.Property(p => p.Status).HasColumnName("Status").IsRequired();
            this.Property(p => p.AccessToken).HasColumnName("AccessToken").IsRequired();
        }
    }
}
