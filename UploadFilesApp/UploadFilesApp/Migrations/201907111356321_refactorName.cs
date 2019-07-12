namespace UploadFilesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactorName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FileVersionModels", newName: "MaterialVersionModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MaterialVersionModels", newName: "FileVersionModels");
        }
    }
}
