namespace Football_Club_info__1273288_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clubs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClubName = c.String(nullable: false, maxLength: 50),
                        ClubLogo = c.String(nullable: false, maxLength: 50),
                        Country = c.String(nullable: false, maxLength: 50),
                        StadiumName = c.String(nullable: false, maxLength: 50),
                        Capacity = c.Int(nullable: false),
                        FoundedDate = c.DateTime(nullable: false, storeType: "date"),
                        Manager = c.String(nullable: false, maxLength: 50),
                        League = c.Int(nullable: false),
                        IsChampionLeagueWinner = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(nullable: false, maxLength: 50),
                        Nationality = c.String(nullable: false, maxLength: 50),
                        Age = c.Int(nullable: false),
                        JerseyNumber = c.Int(nullable: false),
                        Debut = c.DateTime(nullable: false, storeType: "date"),
                        ClubId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clubs", t => t.ClubId, cascadeDelete: true)
                .Index(t => t.ClubId);
                CreateStoredProcedure("dbo.DeleteClub",
                p => new { id = p.Int() },
                "DELETE FROM dbo.Clubs WHERE id=@id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "ClubId", "dbo.Clubs");
            DropIndex("dbo.Players", new[] { "ClubId" });
            DropTable("dbo.Players");
            DropTable("dbo.Clubs");
            DropStoredProcedure("dbo.DeleteClub");
        }
    }
}
