namespace Managing_Teacher_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToTeacherTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teacher", "UserId", c => c.Int(nullable: true));
            CreateIndex("dbo.Teacher", "UserId");
            AddForeignKey("dbo.Teacher", "UserId", "dbo.User", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teacher", "UserId", "dbo.User");
            DropIndex("dbo.Teacher", new[] { "UserId" });
            DropColumn("dbo.Teacher", "UserId");
        }
    }
}
