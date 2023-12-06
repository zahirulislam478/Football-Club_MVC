namespace Football_Club_info__1273288_.Migrations
{
    using Football_Club_info__1273288_.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Football_Club_info__1273288_.Models.FootballDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Football_Club_info__1273288_.Models.FootballDbContext db) 
        {
            Club c1 = new Club
            {
                Id = 1,
                ClubName = "Manchester United",
                ClubLogo = "4.jpg",
                Country = "England",
                StadiumName = "Old Trafford",
                Capacity = 74800,
                FoundedDate = new DateTime(1878, 4, 5),
                Manager = "Erik ten Hag",
                League = League.PremierLeague,
                IsChampionLeagueWinner = true
            };
            c1.Players.Add(new Player
            {
                PlayerName = "Erling Haaland",
                Age = 21,
                Nationality = "Norway",
                JerseyNumber = 9,
                Debut = new DateTime(2014, 7, 21)
            });
            c1.Players.Add(new Player
            {
                PlayerName = "Bruno Fernandes",
                Age = 26,
                Nationality = "Portugal",
                JerseyNumber = 18,
                Debut = new DateTime(2016, 9, 8)
            });
            db.Clubs.Add(c1);
            db.SaveChanges();
        }
    }
}
