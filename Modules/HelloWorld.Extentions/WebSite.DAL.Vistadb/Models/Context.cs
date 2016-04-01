using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebSite.Common.Models.ViewModels;
using WebSite.DAL.Db.Models.Mapping;
using WebSite.DAL.Db.Models.Models;
using WebSite.DAL.Db.Models.Models.Mapping;
using WebSite.DAL.Vistadb.Models.Mapping;

namespace WebSite.DAL.Db.Models
{
    internal partial class Context : DbContext
    {
        static Context()
        {
            Database.SetInitializer<Context>(null);
        }

        public Context(string connectionString)
            :base(connectionString)
        {
        }

        public Context()
            : base("Name=Context")
        {
        }

        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<ChatSessionMessage> ChatSessionMessages { get; set; }
        public DbSet<OfflineMessage> OfflineMessages { get; set; }
        public DbSet<CourseGroupDbM> CourseGroups { get; set; }
        public DbSet<FeedbackDbModel> Feedbacks { get; set; }
        public DbSet<CourseDbM> Courses { get; set; }
        public DbSet<StudentDbM> Students { get; set; }
        public DbSet<CoursePriceDbM> CoursePrices { get; set; }
        public DbSet<CourseInfoDbM> CourseInfo { get; set; }
        public DbSet<PageLayoutDbM> PageLayouts { get; set; }
        public DbSet<MainPageItemDbM> MainPageItems { get; set; }
        public DbSet<SignupApplicationDbModel> SignupApplications { get; set; }
        public DbSet<CanceledSignupApplicationDbModel> CanceledSignupApplications { get; set; }


        public DbSet<FlowDbModel> Flows { get; set; }
        public DbSet<EmailDbModel> Emails { get; set; }
        public DbSet<EmailTemplateDbModel> EmailTemplates { get; set; }
        public DbSet<ScheduledPaymentDbM> ScheduledPayments { get; set; }
        public DbSet<ActualPaymentDbM> ActualPayments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ChatSessionMap());
            modelBuilder.Configurations.Add(new ChatSessionMessageMap());
            modelBuilder.Configurations.Add(new OfflineMessageMap());
            modelBuilder.Configurations.Add(new CourseGroupMap());
            modelBuilder.Configurations.Add(new CourseMap());
            modelBuilder.Configurations.Add(new CourseInfoMap());
            modelBuilder.Configurations.Add(new PageLayoutMap());
            modelBuilder.Configurations.Add(new MainPageItemMap());
            modelBuilder.Configurations.Add(new FlowMap());
            modelBuilder.Configurations.Add(new SignupApplicationMap());
            modelBuilder.Configurations.Add(new CanceledSignupApplicationMap());
            modelBuilder.Configurations.Add(new EmailMap());
            modelBuilder.Configurations.Add(new EmailRecipientMap());
            modelBuilder.Configurations.Add(new EmailTemplateMap());
            modelBuilder.Configurations.Add(new CoursePriceMap());
            modelBuilder.Configurations.Add(new FeedBackMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new ScheduledPaymentMap());
            modelBuilder.Configurations.Add(new ActualPaymentMap());
        }

    }
}
