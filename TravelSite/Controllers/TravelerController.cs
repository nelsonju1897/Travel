﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSite.Models;

namespace TravelSite.Controllers
{
    public class TravelerController : Controller
    {
        public ApplicationDbContext db;
        public TravelerController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Traveler
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ViewItineraries()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Travelers.Include("Itineraries").FirstOrDefault(t => t.ApplicationUserId == userId).Itineraries.ToList());
        }
        // GET: Traveler/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Traveler/Create
        [HttpPost]
        public ActionResult Create(Traveler Traveler)
        {
            Traveler.Id = Guid.NewGuid();
                Traveler.ApplicationUserId = User.Identity.GetUserId();
                db.Travelers.Add(Traveler);
                db.SaveChanges();
                return RedirectToAction("GetInterests");

        }
        [HttpGet]
        public ActionResult GetInterests()
        {
            return View(db.Interests.ToList());
        }
        [HttpPost]
        public ActionResult GetInterests(List<Interest> interests)
        {
            var userId = User.Identity.GetUserId();
            Traveler Traveler = db.Travelers.FirstOrDefault(t => t.ApplicationUserId == userId);
            foreach(Interest interest in interests)
            {
                if(interest.isChecked)
                Traveler.Interests.Add(db.Interests.FirstOrDefault(i => i.Id == interest.Id));
            }
            db.SaveChanges();
            return View("Index");
        }

        [HttpGet]
        public ActionResult EditInterests()
        {
            return View(db.Interests.ToList());
        }
        [HttpPost]
        public ActionResult EditInterests(List<Interest> interests)
        {
            var userId = User.Identity.GetUserId();
            Traveler traveler = db.Travelers.Include("Interests").First(t => t.ApplicationUserId == userId);
            for (int i = 0; i < traveler.Interests.Count; i++)
            {
                if (!interests.Contains(traveler.Interests.ToList()[i]))
                {
                    traveler.Interests.Remove(traveler.Interests.ToList()[i]);
                    i--;
                }
            }
            foreach (Interest i in interests)
            {
                if (!traveler.Interests.Contains(i) && i.isChecked)
                {
                    traveler.Interests.Add(db.Interests.FirstOrDefault(interest => interest.Id == i.Id));
                }
            }
            db.SaveChanges();
            return View("Index");
        }
        // GET: Traveler/Edit/5
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Travelers.FirstOrDefault(t => t.ApplicationUserId == userId));
        }

        // POST: Traveler/Edit/5
        [HttpPost]
        public ActionResult Edit(Traveler Traveler)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                Traveler TravelerFromDb = db.Travelers.FirstOrDefault(t => t.ApplicationUserId == userId);
                TravelerFromDb.FirstName = Traveler.FirstName;
                TravelerFromDb.LastName = Traveler.LastName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
