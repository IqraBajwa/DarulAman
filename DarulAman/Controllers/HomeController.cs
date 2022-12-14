using DarulAman.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DarulAman.Utills;

namespace DarulAman.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult IndexAdmin()
        {
            return View();
        }

        public ActionResult IndexCustomer()
        {
           

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
        public ActionResult About()
        {
           
            return View();
        }

        public ActionResult SupportUs()
        {
            return View();
        }
        public ActionResult Slider()
        {
            return View(db.tbl_Slider.ToList());
           
        }
        public ActionResult Donate()
        {

            return View();
        }
        public ActionResult Team()
        {

            return View(db.tbl_Team.ToList());
        }  
        public ActionResult Event()
        {

            return View(db.tbl_Event.ToList());
        } 
        public ActionResult Teaching()
        {

            return View(db.tbl_TeachingCourse.ToList());
        }
       

        public ActionResult Marriage()
        {

            return View();
        }
      
       
        public ActionResult DeadRelative()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DeadRelative(tbl_DeadRelative DeadRelative)
        {
            if (ModelState.IsValid)

            {
                DeadRelative.DATE = System.DateTime.Now;
                db.tbl_DeadRelative.Add(DeadRelative);
                db.SaveChanges();

                var max = db.tbl_DeadRelative.Max(x => x.FUNERAL_ID);
                //Max Means to Fetch LastID in the Current Table
                return Redirect("/Home/Decreased/" + max);
            }
            string mailBody = "Your request have been receievd. We Will contact you soon.\n Thanks";
            EmailProvider.Email(DeadRelative.EMAIL, "CONFIRMATION MESSAGE", mailBody);
            return RedirectToAction("Confirmation");
           
        }
        public ActionResult Decreased(int id)
        {
            ViewBag.LastID = id;
            return View();
        }
        [HttpPost]
        public ActionResult Decreased(tbl_Decreased d)
        {
                if (ModelState.IsValid)
            {
                d.DATE = System.DateTime.Now;
                db.tbl_Decreased.Add(d);
                db.SaveChanges();
               }
            return View();
        }

    public ActionResult School()
        {

            return View(db.tbl_SchoolCourse.ToList());
        }
        public ActionResult Prayer()
        {

            return View();
        } 
        public ActionResult Volunteer()
        {

            return View();
        }
        public ActionResult Mosque()
        {

            return View();
        } 
        public ActionResult Library()
        {

            return View();
        }
        public ActionResult GeneralRegistration()
        {

            return View();
        }  
        public ActionResult Translation()
        {

            return View();
        }
        public ActionResult Gym()
        {

            return View();
        }
        public ActionResult Slaughterhouse()
        {

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_Admin a)
        {

            var check = db.tbl_Admin.Where(x => x.EMAIL == a.EMAIL && x.PASSWORD == a.PASSWORD).FirstOrDefault();
            if (check != null)
            {
                if (check.STATUS == "Active")
                {

                    Session["Admin"] = check;
                    return RedirectToAction("IndexAdmin", "Home");
                }
               
                else
                {
                    ViewBag.Message = "Your Account have not been active anymore.\n Please contact Admin";
                    return View();
                }
            }

            else
            {
                ViewBag.Message = "Invalid Email & Password";
                return View();
            }


        }

        public ActionResult Logout()
        {
            Session["Admin"] = null;
         return RedirectToAction("Login");
        }
        public ActionResult Newpassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Newpassword(string newPassword)
        {
            var pass = (tbl_Admin)Session["ForgetPassword"];
            var v = db.tbl_Admin.Where(x => x.EMAIL == pass.EMAIL).FirstOrDefault();
            v.PASSWORD = newPassword;
            db.Entry(v).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Password has Updated Successfully";
            return RedirectToAction("Login");

        }
        public ActionResult CodeVerify()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CodeVerify(int? code)
        {
            int sendCode = (int)Session["code"];
            if (code == sendCode)
            {
                return RedirectToAction("Newpassword");
            }
            TempData["message"] = "Invalid Email";
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {
            var pass = db.tbl_Admin.Where(x => x.EMAIL == email).FirstOrDefault();

            if (pass == null)
            {
                TempData["Error"] = "Invalid Email, Please!\n Enter Valid Email";
                return RedirectToAction("Login");
            }

            Random random = new Random();
            int Code = random.Next(1001, 9999);
            Session["code"] = Code;
            Session["ForgetPassword"] = pass;
            Email.SendEmail(pass.EMAIL, Code);

            return RedirectToAction("CodeVerify");
        }

        [HttpPost]
        public ActionResult Teaching(tbl_TeachingRegistration t)
        {
            if (ModelState.IsValid)
            {
                t.DATE = System.DateTime.Now;
                db.tbl_TeachingRegistration.Add(t);
                db.SaveChanges();  
            }
            string mailBody = "Your request have been receievd. We Will contact you soon.\n Thanks";
            EmailProvider.Email(t.EMAIL, "CONFIRMATION MESSAGE", mailBody);
            return RedirectToAction("Confirmation");
        }
        public ActionResult Confirmation()
        {          
            return View();
        }
        [HttpPost]
        public ActionResult School(tbl_SchoolRegistration s)
        {
            if (ModelState.IsValid)
            {
                s.DATE = System.DateTime.Now;
                db.tbl_SchoolRegistration.Add(s);
                db.SaveChanges();
            }
            string mailBody = "Your request have been receievd. We Will contact you soon.\n Thanks";
            EmailProvider.Email(s.EMAIL, "CONFIRMATION MESSAGE", mailBody);
            return RedirectToAction("Confirmation");
        }
        [HttpPost]
        public ActionResult Marriage(tbl_Marriage m)
        {

            if (ModelState.IsValid)
            {
                m.DATE = System.DateTime.Now;
               db.tbl_Marriage.Add(m);
                db.SaveChanges();
            }
            string mailBody = "Your request have been receievd. We Will contact you soon.\n Thanks";
            EmailProvider.Email(m.EMAIL, "CONFIRMATION MESSAGE", mailBody);
            return RedirectToAction("Confirmation");

        }
        [HttpPost]
        public ActionResult GeneralReg(tbl_GeneralRegistrationForm g)
        {
            if (ModelState.IsValid)
            {
                g.DATE = System.DateTime.Now;
                db.tbl_GeneralRegistrationForm.Add(g);
                db.SaveChanges();
            }
            string mailBody = "Your request have been receievd. We Will contact you soon.\n Thanks";
            EmailProvider.Email(g.EMAIL, "CONFIRMATION MESSAGE", mailBody);
            return RedirectToAction("Confirmation");
        }
   
        public ActionResult PrivacyPolicy()
        {
            return View();
        } 
        public ActionResult Termsofuse()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Donate(tbl_Donation donation)
        {
            if (ModelState.IsValid)//If all of the require fields for donation filled then it will work
            {
                donation.DATE = System.DateTime.Now;
                db.tbl_Donation.Add(donation);
                db.SaveChanges();
                ViewBag.message = "Your Amount have been delivered. \n Thanks for your Donation";
            }
            return View("IndexCustomer");
        }
       

       
        [HttpPost]
        public ActionResult SubmitContact(tbl_Contact c)
        {
            if (ModelState.IsValid)
            {

                c.DATE = System.DateTime.Now;
                db.tbl_Contact.Add(c);
                db.SaveChanges();
                ViewBag.message = "Your Request have been delivered";
            }
            string mailBody = "Your request have been receievd. We Will contact you soon.\n Thanks";
            EmailProvider.Email(c.EMAIL, "CONFIRMATION MESSAGE", mailBody);
            return RedirectToAction("Confirmation");
        }
        [HttpPost]
        public ActionResult SubmitVolunteer(tbl_Volunteer v)
        {
            if (ModelState.IsValid)
            {
                v.DATE = System.DateTime.Now;
                db.tbl_Volunteer.Add(v);
                db.SaveChanges();
            }
            string mailBody = "Your request have been receievd. We Will contact you soon.\n Thanks";
            EmailProvider.Email(v.EMAIL, "CONFIRMATION MESSAGE", mailBody);
            return RedirectToAction("Confirmation");
        }
         public ActionResult FAQs()
        {
            return View (db.tbl_FAQs.ToList());
           
       }
    }
}