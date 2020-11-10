namespace Managing_Teacher_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoleTeacher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teacher", "RoleId", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Role", "Name", c => c.String(maxLength: 255));
            CreateIndex("dbo.Teacher", "RoleId");
            AddForeignKey("dbo.Teacher", "RoleId", "dbo.Role", "ID");
            DropColumn("dbo.Role", "Name_Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Role", "Name_Role", c => c.String(maxLength: 255));
            DropForeignKey("dbo.Teacher", "RoleId", "dbo.Role");
            DropIndex("dbo.Teacher", new[] { "RoleId" });
            DropColumn("dbo.Role", "Name");
            DropColumn("dbo.Teacher", "RoleId");
        }
    }
}
