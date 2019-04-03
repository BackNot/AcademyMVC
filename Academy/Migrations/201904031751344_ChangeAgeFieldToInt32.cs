namespace Academy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAgeFieldToInt32 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Age", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Age", c => c.String());
        }
    }
}
