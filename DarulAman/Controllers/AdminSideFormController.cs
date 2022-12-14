using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DarulAman.Models;

namespace DarulAman.Controllers
{
    public class AdminSideFormController : Controller
    {
        Model1 db = new Model1();
   
        public ActionResult GeneralRegistration()
        {
            var g = db.tbl_GeneralRegistrationForm.ToList();
            return View(g);
        }  
       
        public ActionResult ContactRegistration()
        {
            var c = db.tbl_Contact.ToList();
            return View(c);
        }
        public ActionResult MyProfile()
        {
            var tbl_Admin = db.tbl_Admin.Include(t => t.tbl_AccessLevel);
            return View(tbl_Admin.ToList());
        }
        // GET: tbl_Admin/Edit/5
        public ActionResult MyProfileEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Admin tbl_Admin = db.tbl_Admin.Find(id);
            if (tbl_Admin == null)
            {
                return HttpNotFound();
            }
            ViewBag.ACCESS_LEVEL_FID = new SelectList(db.tbl_AccessLevel, "ACCESS_LEVEL_ID", "NAME", tbl_Admin.ACCESS_LEVEL_FID);
            return View(tbl_Admin);
        }

        // POST: tbl_Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyProfileEdit([Bind(Include = "ADMIN_ID,FIRST_NAME,LAST_NAME,EMAIL,CONTACT,PASSWORD,ADDRESS,STATUS,ACCESS_LEVEL_FID")] tbl_Admin tbl_Admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyProfile");
            }
            ViewBag.ACCESS_LEVEL_FID = new SelectList(db.tbl_AccessLevel, "ACCESS_LEVEL_ID", "NAME", tbl_Admin.ACCESS_LEVEL_FID);
            return View(tbl_Admin);
        }



    }
}