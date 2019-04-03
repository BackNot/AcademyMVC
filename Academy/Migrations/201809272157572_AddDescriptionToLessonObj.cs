namespace Academy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionToLessonObj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lessons", "Description");
        }
    }
}
