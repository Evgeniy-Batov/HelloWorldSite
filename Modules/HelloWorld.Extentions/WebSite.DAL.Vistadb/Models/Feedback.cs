using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models.Models
{
    public class FeedbackDbModel
    {
        public void SetValues(String studentName, int courseId, String feedback, String imageRef,
            String vkprofileref, String fbprofileref, String okprofileref, DateTime posttime)
        {
            this.StudentName  = studentName;
            this.CourseId     = courseId;
            this.FeedBack     = feedback;
            this.ImageRef     = imageRef;
            this.VKProfileRef = vkprofileref;
            this.FBProfileRef = fbprofileref;
            this.OKProfileRef = okprofileref;
            this.PostTime     = posttime;
        }


        [Key,DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id              { get;  set; }
        public String StudentName  { get;  set; }
        public int CourseId        { get;  set; }
        public String FeedBack     { get;  set; }
        public String ImageRef     { get;  set; }
        public String VKProfileRef { get;  set; }
        public String FBProfileRef { get;  set; }
        public String OKProfileRef { get;  set; }
        public DateTime PostTime   { get;  set; }

        public virtual CourseDbM Course { get; protected set; }
    }

    public class FeedBackMap : EntityTypeConfiguration<FeedbackDbModel>
    {
        public FeedBackMap()
        {
            this.ToTable("FeedBacks","evgeniybatov");

            this.HasKey(f => f.Id);
            this.Property(f => f.StudentName).IsRequired();
            this.Property(f => f.CourseId).IsRequired();
            this.Property(f => f.FeedBack).IsRequired();

            this.Property(f => f.Id).HasColumnName("Id");
            this.Property(f => f.StudentName).HasColumnName("StudentName");
            this.Property(f => f.CourseId).HasColumnName("CourseId");
            this.Property(f => f.FeedBack).HasColumnName("FeedBack");
            this.Property(f => f.ImageRef ).HasColumnName("ImageRef");
            this.Property(f => f.VKProfileRef).HasColumnName("VKProfileRef");
            this.Property(f => f.FBProfileRef).HasColumnName("FBProfileRef");
            this.Property(f => f.OKProfileRef).HasColumnName("OKProfileRef");
            this.Property(f => f.PostTime).HasColumnName("PostTime");

            this.HasRequired(p => p.Course).WithMany().HasForeignKey(p => p.CourseId);
        }
    }
}
