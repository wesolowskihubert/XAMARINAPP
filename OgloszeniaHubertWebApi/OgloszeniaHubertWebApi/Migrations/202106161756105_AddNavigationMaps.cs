namespace OgloszeniaHubertWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNavigationMaps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OgloszeniaUsers", "Lat", c => c.Double(nullable: false));
            AddColumn("dbo.OgloszeniaUsers", "Lon", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OgloszeniaUsers", "Lon");
            DropColumn("dbo.OgloszeniaUsers", "Lat");
        }
    }
}
