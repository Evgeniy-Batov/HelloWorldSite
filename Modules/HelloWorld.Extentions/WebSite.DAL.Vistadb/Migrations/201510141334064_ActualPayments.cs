namespace WebSite.DAL.Db.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualPayments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("evgeniybatov.ScheduledPayments", "FlowId", "evgeniybatov.Flow");
            DropForeignKey("evgeniybatov.ScheduledPayments", "StudentId", "evgeniybatov.Student");
            DropIndex("evgeniybatov.ScheduledPayments", new[] { "StudentId" });
            DropIndex("evgeniybatov.ScheduledPayments", new[] { "FlowId" });
            CreateTable(
                "evgeniybatov.ActualPayments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        RegisteredBy = c.String(),
                        IsIncome = c.Boolean(nullable: false),
                        ScheduledPaymentId = c.Int(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("evgeniybatov.ScheduledPayments", t => t.ScheduledPaymentId)
                .Index(t => t.ScheduledPaymentId);
            
            AddColumn("evgeniybatov.ScheduledPayments", "IsRecurrent", c => c.Boolean(nullable: false));
            AddColumn("evgeniybatov.ScheduledPayments", "RecurrentType", c => c.Int());
            AddColumn("evgeniybatov.ScheduledPayments", "RecurrentMoment", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.ScheduledPayments", "IsExpence", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.ScheduledPayments", "Comment", c => c.String());
            AddColumn("evgeniybatov.ScheduledPayments", "StudentDbM_StudentId", c => c.Int());
            AlterColumn("evgeniybatov.ScheduledPayments", "StudentId", c => c.Int());
            AlterColumn("evgeniybatov.ScheduledPayments", "FlowId", c => c.Int());
            CreateIndex("evgeniybatov.ScheduledPayments", "StudentId");
            CreateIndex("evgeniybatov.ScheduledPayments", "FlowId");
            CreateIndex("evgeniybatov.ScheduledPayments", "StudentDbM_StudentId");
            AddForeignKey("evgeniybatov.ScheduledPayments", "FlowId", "evgeniybatov.Flow", "FlowId");
            AddForeignKey("evgeniybatov.ScheduledPayments", "StudentId", "evgeniybatov.Student", "StudentId");
            AddForeignKey("evgeniybatov.ScheduledPayments", "StudentDbM_StudentId", "evgeniybatov.Student", "StudentId");
        }
        
        public override void Down()
        {
            DropForeignKey("evgeniybatov.ScheduledPayments", "StudentDbM_StudentId", "evgeniybatov.Student");
            DropForeignKey("evgeniybatov.ScheduledPayments", "StudentId", "evgeniybatov.Student");
            DropForeignKey("evgeniybatov.ScheduledPayments", "FlowId", "evgeniybatov.Flow");
            DropForeignKey("dbo.ActualPaymentDbMs", "ScheduledPaymentId", "evgeniybatov.ScheduledPayments");
            DropIndex("evgeniybatov.ScheduledPayments", new[] { "StudentDbM_StudentId" });
            DropIndex("evgeniybatov.ScheduledPayments", new[] { "FlowId" });
            DropIndex("evgeniybatov.ScheduledPayments", new[] { "StudentId" });
            DropIndex("dbo.ActualPaymentDbMs", new[] { "ScheduledPaymentId" });
            AlterColumn("evgeniybatov.ScheduledPayments", "FlowId", c => c.Int(nullable: false));
            AlterColumn("evgeniybatov.ScheduledPayments", "StudentId", c => c.Int(nullable: false));
            DropColumn("evgeniybatov.ScheduledPayments", "StudentDbM_StudentId");
            DropColumn("evgeniybatov.ScheduledPayments", "Comment");
            DropColumn("evgeniybatov.ScheduledPayments", "IsExpence");
            DropColumn("evgeniybatov.ScheduledPayments", "RecurrentMoment");
            DropColumn("evgeniybatov.ScheduledPayments", "RecurrentType");
            DropColumn("evgeniybatov.ScheduledPayments", "IsRecurrent");
            DropTable("dbo.ActualPaymentDbMs");
            CreateIndex("evgeniybatov.ScheduledPayments", "FlowId");
            CreateIndex("evgeniybatov.ScheduledPayments", "StudentId");
            AddForeignKey("evgeniybatov.ScheduledPayments", "StudentId", "evgeniybatov.Student", "StudentId", cascadeDelete: true);
            AddForeignKey("evgeniybatov.ScheduledPayments", "FlowId", "evgeniybatov.Flow", "FlowId", cascadeDelete: true);
        }
    }
}
