namespace Academy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalSpaceFieldAndDataAnnotations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "TotalSpaces", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Lessons", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Lessons", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Lessons", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lessons", "Content", c => c.String());
            AlterColumn("dbo.Lessons", "Description", c => c.String());
            AlterColumn("dbo.Lessons", "Name", c => c.String());
            AlterColumn("dbo.Courses", "Description", c => c.String());
            AlterColumn("dbo.Courses", "Name", c => c.String());
            DropColumn("dbo.Courses", "TotalSpaces");
        }
    }
}
