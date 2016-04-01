namespace WebSite.DAL.Db.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newreciepenttypeforsms : DbMigration
    {
        public override void Up()
        {
            AddColumn("evgeniybatov.EmailRecipient", "Phone", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("evgeniybatov.EmailRecipient", "Phone");
        }
    }
}
