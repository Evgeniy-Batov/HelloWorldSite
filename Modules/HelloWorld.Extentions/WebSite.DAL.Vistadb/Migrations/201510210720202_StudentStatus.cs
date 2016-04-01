namespace WebSite.DAL.Db.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("evgeniybatov.Student", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("evgeniybatov.Student", "Status");
        }
    }
}
