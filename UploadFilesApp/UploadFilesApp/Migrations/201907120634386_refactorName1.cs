namespace UploadFilesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactorName1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialVersionModels", "MaterialSize", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialVersionModels", "MaterialSize");
        }
    }
}
