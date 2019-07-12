namespace UploadFilesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileVersionModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MaterialId = c.Guid(nullable: false),
                        VersionNumber = c.Int(nullable: false),
                        MaterialName = c.String(),
                        MaterialUploadDate = c.DateTime(),
                        MaterialType = c.String(),
                        MaterialPath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MaterialModels", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.MaterialModels",
                c => new
                    {
                        MaterialId = c.Guid(nullable: false),
                        MaterialCategory = c.Int(nullable: false),
                        CurrentVersion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileVersionModels", "MaterialId", "dbo.MaterialModels");
            DropIndex("dbo.FileVersionModels", new[] { "MaterialId" });
            DropTable("dbo.MaterialModels");
            DropTable("dbo.FileVersionModels");
        }
    }
}
