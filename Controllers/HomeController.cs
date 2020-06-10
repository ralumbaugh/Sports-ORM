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
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}