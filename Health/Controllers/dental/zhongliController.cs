using Health.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.Controllers.dental
{
    public class zhongliController : Controller
    {
        //
        // GET: /zhongli/
        S4HealthEntities db = new S4HealthEntities();
        public ActionResult doctor()
        {
            IEnumerable<S4doctors> s4 = db.S4doctors.ToList();
            return View(s4);
        }
        public ActionResult detail(int did)
        {
            var d = db.S4doctors.Find(did);
            return View(d);

        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string uacount, string upwd, string shengfen)
        {

            if (ModelState.IsValid)
            {
                switch (shengfen)
                {
                    case "用户":
                        var u = db.S4users.Where(n => n.uacount == uacount && n.upwd == upwd).ToList();
                        if (u.Count <= 0)
                        {
                            ModelState.AddModelError("", "账号或密码错误！");
                        }
                        else
                        {
                            //传姓名
                            Session["uacount"] = uacount;                      
                            string e = Session["uacount"].ToString();
                            var m1 = u.Where(m2 => m2.uacount == e).First();
                            ViewBag.ll = m1.uname;
                            Session["id"] = m1.uid;
                            Response.Redirect("~/zhongna/People?p=" + Session["id"]);
                           
                        }
                        break;
                    case "专家":
                        var m = db.S4doctors.Where(n => n.dname == uacount && n.dpwd == upwd).ToList();
                        if (m.Count <= 0)
                        {
                            ModelState.AddModelError("", "账号或密码错误！");
                        }
                        else
                        {
                            var m1 = m.First();
                            Session["dname"] = m1.dname;
                            return RedirectToAction("houtai", "aijuan");
                        }
                        break;
                    case "管理员":
                        var x = db.S4manager.Where(n => n.macount == uacount && n.mpwd == upwd).ToList();
                        if (x.Count <= 0)
                        {

                            ModelState.AddModelError("", "账号或密码错误！");
                        }
                        else
                        {


                            Session["macount"] = uacount;
                            string a = Session["macount"].ToString();
                            var m1 = x.Where(m2 => m2.macount == a).First();
                            ViewBag.mm = m1.mname;
                            Response.Redirect("~/zhongna/guanliyuan?q=" + ViewBag.mm);
                        }
                        break;
                }
            }
            return View();
        }
        public ActionResult anli()
        {
            IEnumerable<S4anli> ss = db.S4anli.ToList();
            return View(ss);
        }
        public ActionResult resigter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult resigter(S4users user)
        {
            if (ModelState.IsValid)
            {
                db.S4users.Add(user);
                db.SaveChanges();
                return RedirectToAction("doctor");
            }
            return View(user);
        }
        public ActionResult add(S4orders b)
        {
            db.S4orders.Add(b);
            db.SaveChanges();
            return null;
        }
        public ActionResult chat()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
