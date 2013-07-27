namespace ETConsole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nombre_bonito : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        SocialSecurityNumber = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address_AddressId = c.Int(nullable: false),
                        Address_StreetAddress = c.String(maxLength: 150),
                        Address_City = c.String(),
                        Address_State = c.String(),
                        Address_ZipCode = c.String(),
                        Info_Weight_Reading = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Info_Weight_Units = c.String(),
                        Info_Height_Reading = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Info_Height_Units = c.String(),
                        Info_DietryRestrictions = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.SocialSecurityNumber);
            
            CreateTable(
                "dbo.a_table_name",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        LocationName = c.String(nullable: false),
                        Country = c.String(),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.LocationID);
            
            CreateTable(
                "dbo.Lodgings",
                c => new
                    {
                        LodgingId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Owner = c.String(),
                        IsResort = c.Boolean(nullable: false),
                        Destination_DestinationId = c.Int(),
                    })
                .PrimaryKey(t => t.LodgingId)
                .ForeignKey("dbo.a_table_name", t => t.Destination_DestinationId)
                .Index(t => t.Destination_DestinationId);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Identifier = c.Guid(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CostUsd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Identifier);
            
            CreateTable(
                "dbo.LocationPhotos",
                c => new
                    {
                        LocationID = c.Int(nullable: false),
                        Photo = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => t.LocationID)
                .ForeignKey("dbo.a_table_name", t => t.LocationID)
                .Index(t => t.LocationID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.LocationPhotos", new[] { "LocationID" });
            DropIndex("dbo.Lodgings", new[] { "Destination_DestinationId" });
            DropForeignKey("dbo.LocationPhotos", "LocationID", "dbo.a_table_name");
            DropForeignKey("dbo.Lodgings", "Destination_DestinationId", "dbo.a_table_name");
            DropTable("dbo.LocationPhotos");
            DropTable("dbo.Trips");
            DropTable("dbo.Lodgings");
            DropTable("dbo.a_table_name");
            DropTable("dbo.People");
        }
    }
}
