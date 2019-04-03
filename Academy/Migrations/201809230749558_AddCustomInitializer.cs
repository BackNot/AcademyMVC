namespace Academy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomInitializer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Spaces = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PathToLesson = c.String(),
                        LinkedCourse_CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.LessonId)
                .ForeignKey("dbo.Courses", t => t.LinkedCourse_CourseId)
                .Index(t => t.LinkedCourse_CourseId);
            
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "LinkedCourse_CourseId", "dbo.Courses");
            DropIndex("dbo.Lessons", new[] { "LinkedCourse_CourseId" });
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String());
            DropTable("dbo.Lessons");
            DropTable("dbo.Courses");
        }
    }
}
