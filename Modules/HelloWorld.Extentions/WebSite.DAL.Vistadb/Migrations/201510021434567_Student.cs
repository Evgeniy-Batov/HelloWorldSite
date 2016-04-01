namespace WebSite.DAL.Db.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Student : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "evgeniybatov.ScheduledPayments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        DueDate = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        FlowId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("evgeniybatov.Flow", t => t.FlowId, cascadeDelete: true)
                .ForeignKey("evgeniybatov.Student", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.FlowId);
            
            CreateTable(
                "evgeniybatov.Student",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentSince = c.DateTime(nullable: false),
                        StudentTill = c.DateTime(),
                        FlowId = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 64),
                        MiddleName = c.String(maxLength: 128),
                        LastName = c.String(maxLength: 128),
                        Email = c.String(nullable: false),
                        Phone1 = c.String(maxLength: 64),
                        Phone2 = c.String(maxLength: 64),
                        PassportNo = c.String(maxLength: 64),
                        PasspordIssuedDate = c.DateTime(),
                        PassportIssuedAt = c.String(maxLength: 512),
                        PassportIssuedBy = c.String(maxLength: 512),
                        Inn = c.String(maxLength: 128),
                        ContractNo = c.String(maxLength: 64),
                        PaymentModel = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("evgeniybatov.Flow", t => t.FlowId, cascadeDelete: false)
                .Index(t => t.FlowId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("evgeniybatov.ScheduledPayments", "StudentId", "evgeniybatov.Student");
            DropForeignKey("evgeniybatov.Student", "FlowId", "evgeniybatov.Flow");
            DropForeignKey("evgeniybatov.ScheduledPayments", "FlowId", "evgeniybatov.Flow");
            DropIndex("evgeniybatov.Student", new[] { "FlowId" });
            DropIndex("evgeniybatov.ScheduledPayments", new[] { "FlowId" });
            DropIndex("evgeniybatov.ScheduledPayments", new[] { "StudentId" });
            DropTable("evgeniybatov.Student");
            DropTable("evgeniybatov.ScheduledPayments");
        }
    }
}
