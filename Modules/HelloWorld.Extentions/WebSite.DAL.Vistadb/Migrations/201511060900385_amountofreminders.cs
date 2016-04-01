namespace WebSite.DAL.Db.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amountofreminders : DbMigration
    {
        public override void Up()
        {
            AddColumn("evgeniybatov.ScheduledPayments", "NumberOfSentReminders", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("evgeniybatov.ScheduledPayments", "NumberOfSentReminders");
        }
    }
}
