namespace RealtimeSample.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DevTests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CampaignName = c.String(maxLength: 255, unicode: false),
                        Date = c.DateTime(nullable: false),
                        Clicks = c.Int(nullable: false),
                        Conversions = c.Int(nullable: false),
                        Impressions = c.Int(nullable: false),
                        AffiliateName = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DevTests");
        }
    }
}
