namespace WebSite.DAL.Db.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payments2 : DbMigration
    {
        public override void Up()
        {
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
                PaymentType = c.Int()
            })
            .PrimaryKey(t => t.PaymentId)
            .ForeignKey("evgeniybatov.ScheduledPayments", t => t.ScheduledPaymentId)
            .Index(t => t.ScheduledPaymentId);
        }
        
        public override void Down()
        {
            MoveTable(name: "evgeniybatov.ActualPayments", newSchema: "dbo");
            RenameTable(name: "dbo.ActualPayments", newName: "ActualPaymentDbMs");
        }
    }
}
