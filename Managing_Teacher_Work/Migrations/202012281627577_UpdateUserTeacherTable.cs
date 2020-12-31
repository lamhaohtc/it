namespace Managing_Teacher_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTeacherTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teacher", "UserId", "dbo.User");
            DropIndex("dbo.Teacher", new[] { "UserId" });
            AddColumn("dbo.User", "TeacherId", c => c.Int(nullable: true));
            CreateIndex("dbo.User", "TeacherId");
            AddForeignKey("dbo.User", "TeacherId", "dbo.Teacher", "ID");
            DropColumn("dbo.Teacher", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teacher", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.User", "TeacherId", "dbo.Teacher");
            DropIndex("dbo.User", new[] { "TeacherId" });
            DropColumn("dbo.User", "TeacherId");
            CreateIndex("dbo.Teacher", "UserId");
            AddForeignKey("dbo.Teacher", "UserId", "dbo.User", "ID");
        }
    }
}
