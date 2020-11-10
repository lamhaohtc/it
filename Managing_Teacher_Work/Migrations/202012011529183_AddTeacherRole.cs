namespace Managing_Teacher_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeacherRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teacher", "RoleId", c => c.String());
            AddColumn("dbo.Role", "Teacher_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Role", "Teacher_ID");
            AddForeignKey("dbo.Role", "Teacher_ID", "dbo.Teacher", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Role", "Teacher_ID", "dbo.Teacher");
            DropIndex("dbo.Role", new[] { "Teacher_ID" });
            DropColumn("dbo.Role", "Teacher_ID");
            DropColumn("dbo.Teacher", "RoleId");
        }
    }
}
