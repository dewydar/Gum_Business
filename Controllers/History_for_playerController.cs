using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymBusiness.Models;
using GymBusiness.Models.ViewModels;
using Newtonsoft.Json;

namespace GymBusiness.Controllers
{
    //[Authorize]
    public class History_for_playerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: History_for_player
        public ActionResult Index()
        {
            HistoryPlayersLoadsVM vm=new HistoryPlayersLoadsVM();
            var history_for_player = db.History_for_player.Include(h => h.Load).Include(h => h.Player);
            return View(history_for_player.ToList());
        }

        // GET: History_for_player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History_for_player history_for_player = db.History_for_player.Find(id);
            if (history_for_player == null)
            {
                return HttpNotFound();
            }
            return View(history_for_player);
        }

        // GET: History_for_player/Create
        public ActionResult Create()
        {
            ViewBag.Loads_id = new SelectList(db.Loads, "ID", "ID");
            ViewBag.Players = new SelectList(db.Players, "ID", "Full_Name");
            return View();
        }

        // POST: History_for_player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( HistoryPlayersLoadsVM history)
        {
            if (ModelState.IsValid)
            {
                db.Loads.Add(history.L);
                if (db.SaveChanges() > 0)
                {
                    history.H.Loads_id = history.L.ID;
                    db.History_for_player.Add(history.H);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            //ViewBag.Loads_id = new SelectList(db.Loads, "ID", "ID", history_for_player.Loads_id);
            ViewBag.Player_id = new SelectList(db.Players, "ID", "Full_Name", history.H.Player_id);
            return View(history);
        }

        // GET: History_for_player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History_for_player history_for_player = db.History_for_player.Find(id);
            if (history_for_player == null)
            {
                return HttpNotFound();
            }
            ViewBag.Loads_id = new SelectList(db.Loads, "ID", "ID", history_for_player.Loads_id);
            ViewBag.Player_id = new SelectList(db.Players, "ID", "Full_Name", history_for_player.Player_id);
            return View(history_for_player);
        }

        // POST: History_for_player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Datetime,Player_id,Weight,Loads_id")] History_for_player history_for_player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(history_for_player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Loads_id = new SelectList(db.Loads, "ID", "ID", history_for_player.Loads_id);
            ViewBag.Player_id = new SelectList(db.Players, "ID", "Full_Name", history_for_player.Player_id);
            return View(history_for_player);
        }

        // GET: History_for_player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History_for_player history_for_player = db.History_for_player.Find(id);
            if (history_for_player == null)
            {
                return HttpNotFound();
            }
            return View(history_for_player);
        }
        public ActionResult Reports(int id)
        {
            ChartVM vm = new ChartVM();
            vm.ID = id;
            ViewBag.Playername = db.Players.Find(id).Full_Name;
            return View(vm);
        }

        public ActionResult History(int id)
        {
            ChartVM vm = new ChartVM();
            ViewBag.Playername = db.Players.Find(id).Full_Name;
            vm.History = db.History_for_player.OrderBy(a => a.Datetime).Where(a => a.Player_id == id).ToList();
            return View(vm);
        }
        //public ContentResult JSON(int id)
        //{
        //    //List<History_for_player> dataPoints = new List<History_for_player>();
        //    //Random random = new Random();
        //    //double y = yStart;

        //    //for (int i = 0; i < length; i++)
        //    //{
        //    //    y = y + random.Next(-1, 2);
        //    //    dataPoints.Add(new History_for_player());
        //    //}
        //    ChartVM vm = new ChartVM();
        //    var data = db.History_for_player.Where(x => x.Player_id == id).ToList();
        //    List<int> Days = new List<int>();
        //    var weight = data.Select(a => a.Weight).Distinct().ToList();
        //    foreach (var item in weight)
        //    {
        //        Days.Add(data.Count(a => a.Weight == item));
        //    }
        //    vm.Dayes = Days;
        //    vm.Weight = weight;
        //    return Content(JsonConvert.SerializeObject(vm, _jsonSetting), "application/json");
        //}

        //JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        // POST: History_for_player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            History_for_player history_for_player = db.History_for_player.Find(id);
            db.History_for_player.Remove(history_for_player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ContentResult JSON(int playerid)
        {
            var playerhistory=db.History_for_player.Where(x=>x.Player_id== playerid).OrderBy(x => x.Datetime);
            //leghnth
            int DayesCount = playerhistory.Count();
            //Dayes
          
            
            var Wights = playerhistory.Select(xxx=>xxx.Weight).ToList();
            
            List<DataPoint> dataPoints = new List<DataPoint>();
            for (int i = 1; i <= DayesCount; i++)
            {
                dataPoints.Add(new DataPoint(i, Wights[i-1]));


            }

            return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");
        }

        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

    

    protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
