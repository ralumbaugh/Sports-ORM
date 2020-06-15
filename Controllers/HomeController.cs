using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsORM.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context context;

        public HomeController(Context DBContext)
        {
            context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = context.Leagues
                .Where(l => l.Sport.Contains("Baseball"));
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.WomensLeagues = context.Leagues
                .Where(l => l.Name.ToLower().Contains("women"));
            ViewBag.HockeySport = context.Leagues
                .Where(l => l.Sport.ToLower().Contains("hockey"));
            ViewBag.FootballInMyLeagueNoThankYou = context.Leagues
                .Where(l => l.Sport.ToLower() != "football");
            ViewBag.ConferenceLeagues = context.Leagues
                .Where(l => l.Name.ToLower().Contains("conference"));
            ViewBag.AtlanticLeagues = context.Leagues
                .Where(l => l.Name.ToLower().Contains("atlantic"));
            ViewBag.DallasTeams = context.Teams
                .Where(t => t.Location.ToLower() == "dallas");
            ViewBag.RaptorTeams = context.Teams
                .Where(t => t.TeamName.ToLower().Contains("raptor"));
            ViewBag.CityTeams = context.Teams
                .Where(t => t.Location.ToLower().Contains("city"));
            ViewBag.TTeams = context.Teams
                .Where(t => t.TeamName.ToLower()[0] == 't' );
            ViewBag.AllAlphabetical = context.Teams
                .OrderBy(t => t.Location);
            ViewBag.ReverseAlphabetical = context.Teams
                .OrderByDescending(t => t.TeamName);
            ViewBag.CooperPlayers = context.Players
                .Where(p => p.LastName.ToLower() == "cooper");
            ViewBag.JoshuaPlayers = context.Players
                .Where(p => p.FirstName.ToLower() == "joshua");
            ViewBag.CooperPlayersExceptForYouJoshYouArentAllowed = context.Players
                .Where(p => p.LastName.ToLower() == "cooper" && p.FirstName.ToLower() != "joshua");
            ViewBag.AlexandersOrWyatts = context.Players
                .Where(p => p.FirstName.ToLower() == "alexander" || p.FirstName.ToLower() == "wyatt");
            return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            ViewBag.AtlanticSoccerTeams = context.Teams
                .Include(t => t.CurrLeague)
                .Where(t => t.CurrLeague.Name.ToLower() == "atlantic soccer conference")
                .ToList();
            ViewBag.BostonPenguinsPlayers = context.Players
                .Include(p => p.CurrentTeam)
                .Where(t => t.CurrentTeam.TeamName.ToLower()=="penguins" && t.CurrentTeam.Location.ToLower()=="boston")
                .ToList();
            ViewBag.InternationalCollegiateBaseballConference = context.Teams
                .Include(l => l.CurrLeague)
                .Where(l => l.CurrLeague.Name.ToLower() == "international collegiate baseball conference")
                .ToList();
            ViewBag.AmericanConferenceofAmateurFootballTeams = context.Teams
                .Include(l => l.CurrLeague)
                .Where(l => l.CurrLeague.Name.ToLower() == "american conference of amateur football")
                .ToList();
            ViewBag.FootballTeams = context.Teams
                .Include(l => l.CurrLeague)
                .Where(l => l.CurrLeague.Sport.ToLower() == "football")
                .ToList();
            ViewBag.SophiaTeams = context.Players
                .Where(p => p.FirstName.ToLower() == "sophia" || p.LastName.ToLower() == "sophia")
                .Include(t => t.CurrentTeam)
                .ToList();
            ViewBag.FloresTeams = context.Players
                .Where(t => t.LastName.ToLower() == "flores")
                .Include(t => t.CurrentTeam)
                .ToList();
            ViewBag.ManitobaTigerCats = context.Players
                .Include(t => t.CurrentTeam)
                .Where(t => t.CurrentTeam.Location.ToLower() == "manitoba" && t.CurrentTeam.TeamName.ToLower() == "tiger-cats")
                .ToList();
            ViewBag.BigTeams = context.Teams
                .Where(t => t.AllPlayers.Count() >= 12)
                .ToList();
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            ViewBag.BaileyTeams = context.Players
                .Where(p=> p.FirstName.ToLower() == "alexander" && p.LastName.ToLower() == "bailey")
                .Include(t => t.AllTeams)
                .ThenInclude(ts => ts.TeamOfPlayer)
                .ToList();
            ViewBag.ManitobaTigerCatsPlayers = context.Teams
                .Where(t=> t.TeamName.ToLower() == "tiger-cats" && t.Location.ToLower() == "manitoba")
                .Include(p => p.AllPlayers)
                .ThenInclude(ps => ps.PlayerOnTeam)
                .ToList();
            ViewBag.FormerWichitaVikings = context.Teams
                .Where(t=> t.TeamName.ToLower() == "vikings" && t.Location.ToLower() == "wichita")
                .Include(p => p.AllPlayers)
                .ThenInclude(ps => ps.PlayerOnTeam)
                .ToList();
            ViewBag.EmilysOldTeams = context.Players
                .Where(p=> p.FirstName.ToLower() == "emily" && p.LastName.ToLower() == "sanchez")
                .Include(t => t.AllTeams)
                .ThenInclude(ts => ts.TeamOfPlayer)
                .ToList();
            ViewBag.LevisAnonymous = context.Teams
                .Include(l => l.CurrLeague)
                .Where(league => league.CurrLeague.Name.ToLower() == "atlantic federation of amateur baseball players")
                .Include(p => p.AllPlayers)
                .ThenInclude(ps => ps.PlayerOnTeam)
                .Include(p => p.CurrentPlayers)
                .ToList();
            ViewBag.AllPlayersSorted = context.Players
                .Include(t => t.AllTeams)
                .ThenInclude(ts => ts.TeamOfPlayer)
                .OrderByDescending(teams => teams.AllTeams.Count)
                .ToList();
            return View();
        }

    }
}