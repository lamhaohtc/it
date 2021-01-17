namespace Managing_Teacher_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTeaherActivity : DbMigration
    {
        public override void Up()
        {
            
            DropColumn("dbo.TeacherActivities", "ID");
            DropColumn("dbo.TeacherActivities", "CreatedDate");
            DropColumn("dbo.TeacherActivities", "ModifiedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeacherActivities", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.TeacherActivities", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.TeacherActivities", "ID", c => c.Int(nullable: false));
            DropTable("dbo.Events");
        }
    }
}
