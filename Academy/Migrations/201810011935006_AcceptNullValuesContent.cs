namespace Academy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcceptNullValuesContent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lessons", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lessons", "Content", c => c.String(nullable: false));
        }
    }
}
