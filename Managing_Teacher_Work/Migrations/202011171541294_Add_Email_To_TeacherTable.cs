using System;
using System.Data.Entity.Migrations;

namespace Managing_Teacher_Work.Migrations
{
    
    public partial class Add_Email_To_TeacherTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teacher", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teacher", "Email");
        }
    }
}
