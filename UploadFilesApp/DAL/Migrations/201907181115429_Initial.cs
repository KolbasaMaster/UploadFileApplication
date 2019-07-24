namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaterialModels",
                c => new
                    {
                        MaterialId = c.Guid(nullable: false),
                        MaterialCategory = c.Int(nullable: false),
                        CurrentVersion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialId);
            
            CreateTable(
                "dbo.MaterialVersionModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MaterialId = c.Guid(nullable: false),
                        VersionNumber = c.Int(nullable: false),
                        MaterialName = c.String(),
                        MaterialUploadDate = c.DateTime(),
                        MaterialType = c.String(),
                        MaterialPath = c.String(),
                        MaterialSize = c.Int(nullable: false),
                        UploadId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MaterialModels", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaterialVersionModels", "MaterialId", "dbo.MaterialModels");
            DropIndex("dbo.MaterialVersionModels", new[] { "MaterialId" });
            DropTable("dbo.MaterialVersionModels");
            DropTable("dbo.MaterialModels");
        }
    }
}
