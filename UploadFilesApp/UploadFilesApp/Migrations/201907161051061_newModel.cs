namespace UploadFilesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialVersionModels", "UploadId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialVersionModels", "UploadId");
        }
    }
}
