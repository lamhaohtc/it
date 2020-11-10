using System;
using System.Data.Entity.Migrations;

namespace Managing_Teacher_Work.Migrations
{
    
    public partial class Update_flied_ClassTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Class", "ClassCode", c => c.String(maxLength: 255));
            AddColumn("dbo.Class", "ClassName", c => c.String(maxLength: 255));
            DropColumn("dbo.Class", "Name");
            DropColumn("dbo.Class", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Class", "Address", c => c.String(maxLength: 255));
            AddColumn("dbo.Class", "Name", c => c.String(maxLength: 255));
            DropColumn("dbo.Class", "ClassName");
            DropColumn("dbo.Class", "ClassCode");
        }
    }
}
