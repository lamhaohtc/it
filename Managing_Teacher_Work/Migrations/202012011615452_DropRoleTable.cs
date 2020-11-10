namespace Managing_Teacher_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropRoleTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Role", "Teacher_ID", "dbo.Teacher");
            DropIndex("dbo.Role", new[] { "Teacher_ID" });
            DropColumn("dbo.Teacher", "RoleId");
            DropColumn("dbo.Role", "Teacher_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Role", "Teacher_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Teacher", "RoleId", c => c.String());
            CreateIndex("dbo.Role", "Teacher_ID");
            AddForeignKey("dbo.Role", "Teacher_ID", "dbo.Teacher", "ID");
        }
    }
}
