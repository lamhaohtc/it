namespace Managing_Teacher_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeacherActivityTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "TeacherId", "dbo.Teacher");
            DropIndex("dbo.Activities", new[] { "TeacherId" });
            CreateTable(
                "dbo.TeacherActivities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ActivityId, t.TeacherId })
                .ForeignKey("dbo.Teacher", t => t.TeacherId)
                .ForeignKey("dbo.Activities", t => t.ActivityId)
                .Index(t => t.ActivityId)
                .Index(t => t.TeacherId);
            
            AddColumn("dbo.Activities", "ActivityStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Activities", "TeacherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Activities", "TeacherId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TeacherActivities", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.TeacherActivities", "TeacherId", "dbo.Teacher");
            DropIndex("dbo.TeacherActivities", new[] { "TeacherId" });
            DropIndex("dbo.TeacherActivities", new[] { "ActivityId" });
            DropColumn("dbo.Activities", "ActivityStatus");
            DropTable("dbo.TeacherActivities");
            CreateIndex("dbo.Activities", "TeacherId");
            AddForeignKey("dbo.Activities", "TeacherId", "dbo.Teacher", "ID");
        }
    }
}
