using Football_Club_info__1273288_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Football_Club_info__1273288_.ViewModels
{
    public class ClubViewModel
    {
        public int Id { get; set; } 
        [Required, StringLength(50), DisplayName("Club Name")]
        public string ClubName { get; set; }
        [Required]
        public HttpPostedFileBase ClubLogo { get; set; }
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
        public virtual List<Player> Players { get; set; } = new List<Player>();
    } 
}