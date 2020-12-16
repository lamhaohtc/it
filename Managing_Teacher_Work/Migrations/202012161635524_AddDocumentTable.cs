namespace Managing_Teacher_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocumentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        FilePath = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.ReportMonth");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReportMonth",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        ClassName = c.String(maxLength: 255),
                        Date = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(),
                        Files = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Document");
        }
    }
}
