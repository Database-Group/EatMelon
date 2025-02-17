﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Login.Models;

namespace Login.Controllers
{
    public class TB_USERController : Controller
    {
        private Model3 db = new Model3();

        // GET: TB_USER
        public ActionResult Index()
        {
            return View(db.TB_USER.ToList());
        }

        // GET: TB_USER/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_USER tB_USER = db.TB_USER.Find(id);
            if (tB_USER == null)
            {
                return HttpNotFound();
            }
            return View(tB_USER);
        }

        // GET: TB_USER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TB_USER/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PASSWORD,NAME,STREET_NUM,STREET_NAME,APT_NUM,CITY,PROVINCE,ZIPCODE,PHONE,POINT,CREDIT_NO")] TB_USER tB_USER)
        {
            if (ModelState.IsValid)
            {
                foreach (TB_USER myUser in db.TB_USER)
                {
                    if (myUser.NAME == tB_USER.NAME)
                    {
                        return RedirectToAction("Index");
                    }
                }
                db.TB_USER.Add(tB_USER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_USER);
        }

        // GET: TB_USER/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_USER tB_USER = db.TB_USER.Find(id);
            if (tB_USER == null)
            {
                return HttpNotFound();
            }
            return View(tB_USER);
        }

        // POST: TB_USER/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PASSWORD,NAME,STREET_NUM,STREET_NAME,APT_NUM,CITY,PROVINCE,ZIPCODE,PHONE,POINT,CREDIT_NO")] TB_USER tB_USER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_USER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_USER);
        }

        // GET: TB_USER/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_USER tB_USER = db.TB_USER.Find(id);
            if (tB_USER == null)
            {
                return HttpNotFound();
            }
            return View(tB_USER);
        }

        // POST: TB_USER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TB_USER tB_USER = db.TB_USER.Find(id);
            db.TB_USER.Remove(tB_USER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult LoginCheck()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginCheck(string userName,string passWord)
        {
            if (ModelState.IsValid)
            {
                userName = Request.Form["userName"];
                passWord = Request.Form["passWord"];
                foreach (TB_USER myUser in db.TB_USER)
                {
                    if (myUser.NAME == userName)
                    {
                        if (myUser.PASSWORD == passWord)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
            }
            return View();
        }

        public ActionResult regist()
        {
            return View();
        }
        [HttpPost]
        public ActionResult regist([Bind(Include = "PASSWORD,NAME,DETAILADDR,DISTRICT,CITY,PROVINCE,ZIPCODE,PHONE,CREDIT_NO")] TB_USER tB_USER)
        {
            
                foreach (TB_USER myUser in db.TB_USER)
                {
                    if (myUser.NAME == tB_USER.NAME)
                    {
                        return RedirectToAction("Index");
                    }
                }
                db.TB_USER.Add(tB_USER);
                db.SaveChanges();
                return RedirectToAction("LoginCheck");
            

            return View(tB_USER);
        }
    }
}
