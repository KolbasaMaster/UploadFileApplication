namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MaterialVersionModels", "MaterialType");
            DropColumn("dbo.MaterialVersionModels", "MaterialPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MaterialVersionModels", "MaterialPath", c => c.String());
            AddColumn("dbo.MaterialVersionModels", "MaterialType", c => c.String());
        }
    }
}
