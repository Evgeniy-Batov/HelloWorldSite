namespace WebSite.DAL.Db.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleAndPricesUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("evgeniybatov.Flow", "MondayStart", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "MondayEnd", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "ThuesdayStart", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "ThuesdayEnd", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "WednesdayStart", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "WednesdayEnd", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "ThursdayStart", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "ThursdayEnd", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "FridayStart", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "FridayEnd", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "SaturdayStart", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "SaturdayEnd", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "SundayStart", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.Flow", "SundayEnd", c => c.Int(nullable: false));
            AddColumn("evgeniybatov.CourseAdditionalPrice", "NumberOfMonths", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("evgeniybatov.CourseAdditionalPrice", "NumberOfMonths");
            DropColumn("evgeniybatov.Flow", "SundayEnd");
            DropColumn("evgeniybatov.Flow", "SundayStart");
            DropColumn("evgeniybatov.Flow", "SaturdayEnd");
            DropColumn("evgeniybatov.Flow", "SaturdayStart");
            DropColumn("evgeniybatov.Flow", "FridayEnd");
            DropColumn("evgeniybatov.Flow", "FridayStart");
            DropColumn("evgeniybatov.Flow", "ThursdayEnd");
            DropColumn("evgeniybatov.Flow", "ThursdayStart");
            DropColumn("evgeniybatov.Flow", "WednesdayEnd");
            DropColumn("evgeniybatov.Flow", "WednesdayStart");
            DropColumn("evgeniybatov.Flow", "ThuesdayEnd");
            DropColumn("evgeniybatov.Flow", "ThuesdayStart");
            DropColumn("evgeniybatov.Flow", "MondayEnd");
            DropColumn("evgeniybatov.Flow", "MondayStart");
        }
    }
}
