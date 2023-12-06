using Football_Club_info__1273288_.Models;
using Football_Club_info__1273288_.Repositories.Interfaces;
using Football_Club_info__1273288_.Repositories;
using Football_Club_info__1273288_.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace Football_Club_info__1273288_.Controllers
{
    [Authorize]
    public class ClubsController : Controller
    {
        private readonly FootballDbContext db = new FootballDbContext();
        IGenericRepo<Club> repo;
        public ClubsController()
        {
            this.repo = new GenericRepo<Club>(db);
        }
        // GET: Teams
        [AllowAnonymous]
        public ActionResult Index(int pg = 1)
        {
            //var data = await db.Clubs.OrderBy(a => a.Id).ToPagedListAsync(pg, 5);
            var data = this.repo.GetAll("Players").ToPagedList(pg, 5); 
            return View(data);
        }
        public ActionResult Create()
        {
            ClubViewModel a = new ClubViewModel();
            a.Players.Add(new Player { });
            return View(a);
        }
        [HttpPost]
        public ActionResult Create(ClubViewModel data, string act = "")
        {
            if (act == "add")
            {
                data.Players.Add(new Player { });

                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                data.Players.RemoveAt(index);
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act == "insert")
            {
                if (ModelState.IsValid)
                {
                    var a = new Club
                    {
                        ClubName = data.ClubName,
                        Country = data.Country,
                        StadiumName = data.StadiumName,
                        Capacity = data.Capacity,
                        FoundedDate = data.FoundedDate,
                        Manager = data.Manager,
                        League= data.League,
                        IsChampionLeagueWinner=data.IsChampionLeagueWinner
                    };
                    string ext = Path.GetExtension(data.ClubLogo.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Server.MapPath("~/Pictures/") + fileName;
                    data.ClubLogo.SaveAs(savePath);
                    a.ClubLogo = fileName;
                    foreach (var q in data.Players)
                    {
                        a.Players.Add(q);
                    }
                    this.repo.Insert(a);
                }
            }
            ViewBag.Act = act;
            return PartialView("_CreatePartial", data);
        }
       
        public ActionResult Edit(int id)
        {
            var x = this.repo.Get(id, "Players");
            var a = new ClubEditModel
            {
                  Id = x.Id,
                  ClubName = x.ClubName,
                  Country = x.Country,
                  StadiumName = x.StadiumName,
                  Capacity = x.Capacity,
                  FoundedDate = x.FoundedDate,
                  Manager = x.Manager,
                  League = x.League,
                  IsChampionLeagueWinner = x.IsChampionLeagueWinner,
                  Players = x.Players.ToList()
              };

            ViewBag.CurrentPic = x.ClubLogo;
            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(ClubEditModel data, string act = "")
        {
            if (act == "add")
            {

                data.Players.Add(new Player { });
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }

            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                data.Players.RemoveAt(index);
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }

            if (act == "update")
            {
                if (ModelState.IsValid)
                {
                    var a = db.Clubs.First(x => x.Id == data.Id);

                    a.ClubName = data.ClubName;
                    a.Country = data.Country;
                    a.StadiumName = data.StadiumName;
                    a.Capacity = data.Capacity;
                    a.FoundedDate = data.FoundedDate;
                    a.Manager = data.Manager;
                    a.League = data.League;
                    a.IsChampionLeagueWinner = data.IsChampionLeagueWinner;

                    if (data.ClubLogo != null)
                    {
                        string ext = Path.GetExtension(data.ClubLogo.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Server.MapPath("~/Pictures/") + fileName;
                        data.ClubLogo.SaveAs(savePath);
                        a.ClubLogo = fileName;
                    }
                    a.Players.Clear();
                    this.repo.ExecuteCommand($"DELETE FROM Players WHERE ClubId={a.Id}");
                    this.repo.Update(a);

                    foreach (var item in data.Players)
                    {
                        this.repo.ExecuteCommand($@"INSERT INTO Players([PlayerName],[Nationality],[Age],[JerseyNumber],[Debut],[ClubId]) VALUES('{item.PlayerName}', '{item.Nationality}' ,{item.Age}, {item.JerseyNumber},'{item.Debut}', {a.Id})");
                    }
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CurrentPic = db.Clubs.First(x => x.Id == data.Id).ClubLogo;
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            this.repo.ExecuteCommand($"dbo.DeleteClub {id}");
            return Json(new { success = true, deleted = id });
        }
    }
}