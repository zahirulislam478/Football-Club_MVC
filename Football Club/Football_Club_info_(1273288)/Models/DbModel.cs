using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Football_Club_info__1273288_.Models
{
    public enum League
    {
        PremierLeague = 1,
        LaLiga,
        SerieA,
        Bundesliga,
        Ligue1,
        Eredivisie,
        PrimeiraLiga,
        MLS,
        SaudiProLeague,
        JLeague,
        Superliga,
        IndianSuperLeague,
        BangladeshPremierLeague
    }
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
    public class Club : EntityBase
    {
        [Required, StringLength(50), DisplayName("Club Name")]
        public string ClubName { get; set; }
        [Required, StringLength(50)]
        public string ClubLogo { get; set; }
        [Required, StringLength(50)]
        public string Country { get; set; }
        [Required, StringLength(50)]
        public string StadiumName { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime FoundedDate { get; set; }
        [Required, StringLength(50)]
        public string Manager { get; set; }
        [EnumDataType(typeof(League))]
        public League League { get; set; }
        public bool IsChampionLeagueWinner { get; set; }
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }

    public class Player : EntityBase
    {
        [Required, StringLength(50)]
        public string PlayerName { get; set; }
        [Required, StringLength(50)]
        public string Nationality { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int JerseyNumber { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime Debut { get; set; } 
        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }
    }
    public class FootballDbContext : DbContext
    {
        public FootballDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }  
    }
}