namespace WebSite.DAL.Db.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymentReferences : DbMigration
    {
        public override void Up()
        {
            AddColumn("evgeniybatov.ActualPayments", "StudentId", c => c.Int());
            AddColumn("evgeniybatov.ActualPayments", "FlowId", c => c.Int());
            AddColumn("evgeniybatov.ActualPayments", "CourseId", c => c.Int());
            CreateIndex("evgeniybatov.ActualPayments", "StudentId");
            CreateIndex("evgeniybatov.ActualPayments", "FlowId");
            CreateIndex("evgeniybatov.ActualPayments", "CourseId");
            AddForeignKey("evgeniybatov.ActualPayments", "CourseId", "evgeniybatov.Course", "CourseId");
            AddForeignKey("evgeniybatov.ActualPayments", "FlowId", "evgeniybatov.Flow", "FlowId");
            AddForeignKey("evgeniybatov.ActualPayments", "StudentId", "evgeniybatov.Student", "StudentId");
        }
        
        public override void Down()
        {
            DropForeignKey("evgeniybatov.ActualPayments", "StudentId", "evgeniybatov.Student");
            DropForeignKey("evgeniybatov.ActualPayments", "FlowId", "evgeniybatov.Flow");
            DropForeignKey("evgeniybatov.ActualPayments", "CourseId", "evgeniybatov.Course");
            DropIndex("evgeniybatov.ActualPayments", new[] { "CourseId" });
            DropIndex("evgeniybatov.ActualPayments", new[] { "FlowId" });
            DropIndex("evgeniybatov.ActualPayments", new[] { "StudentId" });
            DropColumn("evgeniybatov.ActualPayments", "CourseId");
            DropColumn("evgeniybatov.ActualPayments", "FlowId");
            DropColumn("evgeniybatov.ActualPayments", "StudentId");
        }
    }
}
