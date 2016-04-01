using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using HelloWorld.Extentions.Models;

namespace HelloWorld.Extentions {
    public class Migrations : DataMigrationImpl {

        public int Create() {

            return 1;
        }


        public int UpdateFrom1()
        {
            return 2;
        }

        public int UpdateFrom2()
        {
            SchemaBuilder.CreateTable(typeof(CoursePartRecord).Name, table => table
              .ContentPartRecord()
              .Column("Name", DbType.String)
              .Column("Title", DbType.String)
              .Column("ShortDescription", DbType.String)
              .Column("SmallRoundImage", DbType.String));

            ContentDefinitionManager.AlterPartDefinition("CoursePart", part => part
                .Attachable());

            return 3;
        }

        public int UpdateFrom3()
        {
            SchemaBuilder.AlterTable(typeof(CoursePartRecord).Name, t =>
            t.DropColumn("SmallRoundImage"));

            return 4;
        }


        public int UpdateFrom4()
        {
            SchemaBuilder.DropTable("dbo_HelloWorld_Extentions_MainPromoRecord");

            return 5;
        }


        public int UpdateFrom5()
        {
            SchemaBuilder.CreateTable(typeof(FeedbackPartRecord).Name, table => table
                 .ContentPartRecord()
                 .Column("CoursePartRecord_Id", DbType.Int32)
                 .Column("AuthorName", DbType.String));

            ContentDefinitionManager.AlterPartDefinition("CoursePart",
                builder => builder.Attachable());

            return 6;
        }

        public int UpdateFrom6()
        {
            ContentDefinitionManager.AlterPartDefinition("FeedbackPart",
            builder => builder.Attachable());

            return 7;
        }

        public int UpdateFrom7()
        {
            SchemaBuilder.CreateTable(typeof(CourseInformationPartRecord).Name, table => table
                  .ContentPartRecord()
                  .Column("Length", DbType.Int32)
                  .Column("LengthPeriod", DbType.String)
                  .Column("LengthText", DbType.String)
                  .Column("Intencity", DbType.Int32)
                  .Column("IntencityPeriod", DbType.String)
                  .Column("IntencityText", DbType.String)
                  .Column("Duration", DbType.Int32)
                  .Column("DurationPeriod", DbType.String)
                  .Column("DurationText", DbType.String)
                  .Column("MinPrice", DbType.Int32)
                  .Column("MinPricePeriod", DbType.String)
                  .Column("MinPriceText", DbType.String)
                  .Column("MaxPrice", DbType.Int32)
                  .Column("MaxPricePeriod", DbType.String)
                  .Column("MaxPriceText", DbType.String));   

            ContentDefinitionManager.AlterPartDefinition("CourseInformationPart", part => part
                .Attachable());



            return 8;
        }

        public int UpdateFrom8()
        {
            SchemaBuilder.AlterTable("FeedbackPartRecord",
                
                table => 
                {
                    table.DropColumn("CoursePartRecord_Id");
                    table.AddColumn<int>("CourseId",c=>c.Nullable());
                }
            );

            return 9;
        }

        public int UpdateFrom9()
        {
            SchemaBuilder.CreateTable(typeof(StudentRegistrationPartRecord).Name, table => table
               .ContentPartRecord()
               .Column("HeaderText", DbType.String)
               .Column("FirstNameLabel", DbType.String)
               .Column("MiddleNameLabel", DbType.String)
               .Column("LastNameLabel", DbType.String)
               .Column("EmailLabel", DbType.String)
               .Column("PhoneLabel", DbType.String)
               .Column("CommentLabel", DbType.String)
           );

            ContentDefinitionManager.AlterPartDefinition(
                typeof(StudentRegistrationPartRecord).Name, cfg => cfg.Attachable());

            return 10;
        }

        public int UpdateFrom10()
        {
            // Create a new widget content type with our map. We make use of the AsWidgetWithIdentity() helper.
            ContentDefinitionManager.AlterTypeDefinition("StudentRegistrationWidget", cfg => cfg
                .WithPart(typeof(StudentRegistrationPartRecord).Name)
                .WithPart("CommonPart")
                .WithPart("WidgetPart")
                .WithSetting("Stereotype", "Widget"));

            return 11;
        }

        public int UpdateFrom11()
        {
            ContentDefinitionManager.AlterPartDefinition("StudentRegistrationPart", part => part
                .Attachable());

            // Create a new widget content type with our map. We make use of the AsWidgetWithIdentity() helper.
            ContentDefinitionManager.AlterTypeDefinition("StudentRegistrationWidget", cfg => cfg
                .WithPart("StudentRegistrationPart"));

            return 12;
        }

		public int UpdateFrom12()
		{
			SchemaBuilder.AlterTable(typeof(StudentRegistrationPartRecord).Name, t => t.AddColumn("RegisterLable", DbType.String));

			return 13;
		}

		public int UpdateFrom13()
		{
			SchemaBuilder.CreateTable(typeof(SessionDataPartRecord).Name, table => table
			   .ContentPartRecord()
			   .Column("PropertyName", DbType.String)
			   .Column("TempData",     DbType.Boolean));


			ContentDefinitionManager.AlterPartDefinition(
				typeof(SessionDataPartRecord).Name, cfg => cfg.Attachable());


			// Create a new widget content type with our map. We make use of the AsWidgetWithIdentity() helper.
			ContentDefinitionManager.AlterTypeDefinition("SessionDataWidget", cfg => cfg
				.WithPart(typeof(SessionDataPartRecord).Name));

			return 14;
		}
	}
}