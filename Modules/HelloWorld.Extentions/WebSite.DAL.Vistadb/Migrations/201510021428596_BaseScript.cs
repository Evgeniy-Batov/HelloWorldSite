namespace WebSite.DAL.Db.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseScript : DbMigration
    {
        public override void Up()
        {
            return;

            CreateTable(
                "evgeniybatov.CanceledSignupApplication",
                c => new
                    {
                        Applicationid = c.Int(nullable: false, identity: true),
                        FlowId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 64),
                        LastName = c.String(nullable: false, maxLength: 64),
                        Email = c.String(nullable: false, maxLength: 64),
                        Phone = c.String(nullable: false, maxLength: 64),
                        Refferal = c.String(nullable: false, maxLength: 64),
                        MondayTime = c.Int(nullable: false),
                        TuesdayTime = c.Int(nullable: false),
                        WednesdayTime = c.Int(nullable: false),
                        ThursdayTime = c.Int(nullable: false),
                        FridayTime = c.Int(nullable: false),
                        SaturdayTime = c.Int(nullable: false),
                        SundayTime = c.Int(nullable: false),
                        AdditionalInfo = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        IP = c.String(),
                        Status = c.Int(nullable: false),
                        AccessToken = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Applicationid)
                .ForeignKey("evgeniybatov.Flow", t => t.FlowId, cascadeDelete: true)
                .Index(t => t.FlowId);
            
            CreateTable(
                "evgeniybatov.Flow",
                c => new
                    {
                        FlowId = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        EstimatedStartDate = c.DateTime(),
                        CustomName = c.String(),
                        ActualStartDate = c.DateTime(),
                        ActualEndDate = c.DateTime(),
                        StartDateStr = c.String(nullable: false, maxLength: 255),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FlowId)
                .ForeignKey("evgeniybatov.Course", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "evgeniybatov.SignupApplication",
                c => new
                    {
                        Applicationid = c.Int(nullable: false, identity: true),
                        FlowId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 64),
                        LastName = c.String(nullable: false, maxLength: 64),
                        Email = c.String(nullable: false, maxLength: 64),
                        Phone = c.String(nullable: false, maxLength: 64),
                        Refferal = c.String(nullable: false, maxLength: 64),
                        MondayTime = c.Int(nullable: false),
                        TuesdayTime = c.Int(nullable: false),
                        WednesdayTime = c.Int(nullable: false),
                        ThursdayTime = c.Int(nullable: false),
                        FridayTime = c.Int(nullable: false),
                        SaturdayTime = c.Int(nullable: false),
                        SundayTime = c.Int(nullable: false),
                        AdditionalInfo = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        IP = c.String(),
                        Status = c.Int(nullable: false),
                        AccessToken = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Applicationid)
                .ForeignKey("evgeniybatov.Flow", t => t.FlowId, cascadeDelete: true)
                .Index(t => t.FlowId);
            
            CreateTable(
                "evgeniybatov.Course",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseGroupId = c.Int(nullable: false),
                        CourseName = c.String(),
                        Breadcrumb = c.String(nullable: false, maxLength: 128),
                        Route = c.String(nullable: false, maxLength: 128),
                        PricePerMonth = c.Int(nullable: false),
                        CourseImageRef = c.String(),
                        WhatIsItHtml = c.String(),
                        WhoRequresIt = c.String(),
                        WhatForHtml = c.String(),
                        HowToAchieve = c.String(),
                        WhatIsRequired = c.String(),
                        Order = c.Int(nullable: false),
                        TitleH1 = c.String(),
                        Description = c.String(),
                        KeyWords = c.String(),
                        Robots = c.String(),
                        PageTitle = c.String(),
                        NextCourse = c.String(),
                        PreviousCourse = c.String(),
                        CustomPageHtml = c.String(),
                        MenuItemStyle = c.String(),
                        IsNew = c.Boolean(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("evgeniybatov.CourseGroup", t => t.CourseGroupId, cascadeDelete: true)
                .Index(t => t.CourseGroupId);
            
            CreateTable(
                "evgeniybatov.CourseAdditionalPrice",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Conditional = c.String(),
                        ShortConditional = c.String(),
                        CustomCSS = c.String(),
                        ValidTill = c.DateTime(),
                    })
                .PrimaryKey(t => t.PriceId)
                .ForeignKey("evgeniybatov.Course", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "evgeniybatov.CourseInfo",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        RenderLength = c.Boolean(nullable: false),
                        LengthHtml = c.String(),
                        RenderSchedule = c.Boolean(nullable: false),
                        ScheduleHtml = c.String(),
                        RenderSyllabus = c.Boolean(nullable: false),
                        SyllabusHtml = c.String(),
                        RenderPrice = c.Boolean(nullable: false),
                        PriceHtml = c.String(),
                        RenderAction = c.Boolean(nullable: false),
                        ActionHtml = c.String(),
                        RenderNews = c.Boolean(nullable: false),
                        NewsHtml = c.String(),
                        RenderSignUp = c.Boolean(nullable: false),
                        SignUpHtml = c.String(),
                        RenderVK = c.Boolean(nullable: false),
                        RenderOK = c.Boolean(nullable: false),
                        RenderFB = c.Boolean(nullable: false),
                        ExtraJS = c.String(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("evgeniybatov.Course", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "evgeniybatov.CourseGroup",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false, maxLength: 128),
                        Breadcrumb = c.String(nullable: false, maxLength: 64),
                        ImagePath = c.String(),
                        GoalsMarkup = c.String(),
                        MethodsMarkup = c.String(),
                        SolutionsMarkup = c.String(),
                        RouteName = c.String(nullable: false, maxLength: 64),
                        Order = c.Int(nullable: false),
                        TitleH1 = c.String(),
                        Description = c.String(),
                        KeyWords = c.String(),
                        Robots = c.String(),
                        PageTitle = c.String(),
                        CustomPageHtml = c.String(),
                        MenuItemStyle = c.String(),
                        IsNew = c.Boolean(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "evgeniybatov.ChatSessionMessages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        SessionId = c.Guid(nullable: false),
                        Author = c.String(nullable: false, maxLength: 32),
                        PostedOn = c.DateTime(nullable: false),
                        MessageText = c.String(nullable: false, maxLength: 255),
                        MessageType = c.Int(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("evgeniybatov.ChatSession", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SessionId);
            
            CreateTable(
                "evgeniybatov.ChatSession",
                c => new
                    {
                        SessionId = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        IP = c.String(),
                        IsBotProcessing = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SessionId);
            
            CreateTable(
                "evgeniybatov.Email",
                c => new
                    {
                        EmailId = c.Int(nullable: false, identity: true),
                        From = c.String(nullable: false, maxLength: 64),
                        Subject = c.String(nullable: false, maxLength: 255),
                        Body = c.String(nullable: false),
                        SentTime = c.DateTime(),
                        Status = c.Int(nullable: false),
                        IsHtml = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmailId);
            
            CreateTable(
                "evgeniybatov.EmailRecipient",
                c => new
                    {
                        RecipientId = c.Int(nullable: false, identity: true),
                        EmailId = c.Int(nullable: false),
                        Recipient = c.String(nullable: false, maxLength: 128),
                        To = c.Boolean(nullable: false),
                        CC = c.Boolean(nullable: false),
                        BCC = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RecipientId)
                .ForeignKey("evgeniybatov.Email", t => t.EmailId, cascadeDelete: true)
                .Index(t => t.EmailId);
            
            CreateTable(
                "evgeniybatov.EmailTemplate",
                c => new
                    {
                        TemplateID = c.Int(nullable: false, identity: true),
                        HtmlVersion = c.String(),
                        TextVerstion = c.String(),
                    })
                .PrimaryKey(t => t.TemplateID);
            
            CreateTable(
                "evgeniybatov.FeedBacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false),
                        CourseId = c.Int(nullable: false),
                        FeedBack = c.String(nullable: false),
                        ImageRef = c.String(),
                        VKProfileRef = c.String(),
                        FBProfileRef = c.String(),
                        OKProfileRef = c.String(),
                        PostTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("evgeniybatov.Course", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "evgeniybatov.MainPageItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        DestinationAction = c.String(),
                        DestinationActionParams = c.String(),
                        DestinationController = c.String(),
                        ImgUrl = c.String(),
                        ItemCss = c.String(),
                        ItemText = c.String(),
                        ItemTitle = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "evgeniybatov.OfflineMessages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                        Topic = c.String(),
                        Message = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        IP = c.String(),
                    })
                .PrimaryKey(t => t.MessageId);
            
            CreateTable(
                "evgeniybatov.PageLayouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageName = c.String(nullable: false, maxLength: 64),
                        PageTitle = c.String(nullable: false, maxLength: 1024),
                        TitleH1 = c.String(nullable: false, maxLength: 1024),
                        Description = c.String(nullable: false),
                        KeyWords = c.String(),
                        Robots = c.String(),
                        Markup = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("evgeniybatov.FeedBacks", "CourseId", "evgeniybatov.Course");
            DropForeignKey("evgeniybatov.EmailRecipient", "EmailId", "evgeniybatov.Email");
            DropForeignKey("evgeniybatov.ChatSessionMessages", "SessionId", "evgeniybatov.ChatSession");
            DropForeignKey("evgeniybatov.CanceledSignupApplication", "FlowId", "evgeniybatov.Flow");
            DropForeignKey("evgeniybatov.Flow", "CourseId", "evgeniybatov.Course");
            DropForeignKey("evgeniybatov.Course", "CourseGroupId", "evgeniybatov.CourseGroup");
            DropForeignKey("evgeniybatov.CourseInfo", "CourseId", "evgeniybatov.Course");
            DropForeignKey("evgeniybatov.CourseAdditionalPrice", "CourseId", "evgeniybatov.Course");
            DropForeignKey("evgeniybatov.SignupApplication", "FlowId", "evgeniybatov.Flow");
            DropIndex("evgeniybatov.FeedBacks", new[] { "CourseId" });
            DropIndex("evgeniybatov.EmailRecipient", new[] { "EmailId" });
            DropIndex("evgeniybatov.ChatSessionMessages", new[] { "SessionId" });
            DropIndex("evgeniybatov.CourseInfo", new[] { "CourseId" });
            DropIndex("evgeniybatov.CourseAdditionalPrice", new[] { "CourseId" });
            DropIndex("evgeniybatov.Course", new[] { "CourseGroupId" });
            DropIndex("evgeniybatov.SignupApplication", new[] { "FlowId" });
            DropIndex("evgeniybatov.Flow", new[] { "CourseId" });
            DropIndex("evgeniybatov.CanceledSignupApplication", new[] { "FlowId" });
            DropTable("evgeniybatov.PageLayouts");
            DropTable("evgeniybatov.OfflineMessages");
            DropTable("evgeniybatov.MainPageItems");
            DropTable("evgeniybatov.FeedBacks");
            DropTable("evgeniybatov.EmailTemplate");
            DropTable("evgeniybatov.EmailRecipient");
            DropTable("evgeniybatov.Email");
            DropTable("evgeniybatov.ChatSession");
            DropTable("evgeniybatov.ChatSessionMessages");
            DropTable("evgeniybatov.CourseGroup");
            DropTable("evgeniybatov.CourseInfo");
            DropTable("evgeniybatov.CourseAdditionalPrice");
            DropTable("evgeniybatov.Course");
            DropTable("evgeniybatov.SignupApplication");
            DropTable("evgeniybatov.Flow");
            DropTable("evgeniybatov.CanceledSignupApplication");
        }
    }
}
