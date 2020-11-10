namespace Managing_Teacher_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ReportMonth_Table : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ReportMonth", "ModifiedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReportMonth", "ModifiedDate", c => c.DateTime());
        }
    }
}
