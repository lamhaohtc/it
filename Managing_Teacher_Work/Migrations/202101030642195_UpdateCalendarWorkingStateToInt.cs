namespace Managing_Teacher_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCalendarWorkingStateToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CalendarWorking", "WorkState", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CalendarWorking", "WorkState", c => c.String(maxLength: 255));
        }
    }
}
