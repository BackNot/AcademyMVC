namespace Academy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContentFieldToLesson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lessons", "Content");
        }
    }
}
