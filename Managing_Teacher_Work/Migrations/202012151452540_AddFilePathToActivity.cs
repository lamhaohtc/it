namespace Managing_Teacher_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilePathToActivity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "FilePath");
        }
    }
}
