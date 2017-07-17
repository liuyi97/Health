using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Models;
using System.Data;

namespace Health.Controllers.dental
{
    public class aijuanController : Controller
    {
        //
        // GET: /aijuan/

        S4HealthEntities db = new S4HealthEntities();
        public ActionResult Index()
        {
            return View();
        }

        //后台
        public ActionResult houtai()
        {
            ViewBag.xx = new SelectList(db.S4ordertype.ToList(), "otid", "otname");
            ViewBag.cc = new SelectList(db.timetype.ToList(), "tiid", "tiname");
            return View();
        }
        /// <summary>
        /// 后台首页显示用户的预约列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getlist1(string tname, string na, int page = 1, int rows = 0)
        {
            var m1 = Session["dname"].ToString();
            var a1 = db.S4doctors.Where(m2 => m2.dname == m1).First();
            int id = a1.did;
            var a2 = db.S4orders.Where(m3 => m3.did == id).ToList();
            List<user> l = new List<user>();
            foreach (var item in a2)
            {
                user u = new user();              
                u.uid = item.uid.ToString();
                u.uname = item.S4users.uname;
                u.uphone = item.S4users.uphone;
                //u.did = db.S4orders.Where(n => n.did == 1).ToString();
                u.dname = item.S4doctors.dname;
                u.tiid = item.tiid.ToString();
                u.tiname = item.timetype.tiname;
                u.shu = item.S4doctors.shu;
                u.otid = item.otid.ToString();
                u.oid = item.oid.ToString();
                u.otname = item.S4ordertype.otname;
                u.tiid = item.timetype.tiid.ToString();
                int a = db.S4orders.Count(n => n.did == 1);
                u.syyy = Convert.ToInt32(item.S4doctors.shu) - a;
                l.Add(u);
            }
            //查找
            if (tname != null && tname != "")
            {
                l = l.Where(n => n.otid == tname).ToList();
            }
            if (na != null && na != "")
            {
                l = l.Where(n => n.tiid == na).ToList();

            }
            ////分页
            int total = l.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    l = l.Take(rows).ToList();
                }
                else
                {
                    l = l.Skip((page - 1) * rows).Take(rows).ToList();
                }
            }
            var obj = new
            {
                total = total,
                rows = l.ToArray()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //回复留言
        public ActionResult talk()
        {
            var f = db.S4liuyan.Where(n=>n.did==liuinfo.zhuanjiaid);
            //List<S4users> u = db.S4users.ToList();
            
            //var obg=from a in f join b in u on a.did equals b.did 
            //        join c in db.S4doctors.ToList() on a.did equals c.did
            //        select new {a.did,a.dtalk,a.lid,a.uid,b.uname,c.dname,a.utalk};
            return View(f);
        }
        [HttpPost]
        public ActionResult talk(S4liuyan s4liuyan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s4liuyan).State = EntityState.Modified;
                db.SaveChanges();
             
            }
            ViewBag.did = new SelectList(db.S4doctors, "did", "dname", s4liuyan.did);
            ViewBag.uid = new SelectList(db.S4users, "uid", "uname", s4liuyan.uid);
            return View(s4liuyan);
        }
        /// <summary>
        /// 回复留言，talk2是跳转传值页面，talk1是保存的方法
        /// </summary>
        /// <param name="id">留言表的主键</param>
        /// <returns></returns>
        /// 留言弹出框
        public ActionResult talk2(int uuid)
        {
            S4liuyan s4liuyan = db.S4liuyan.Find(uuid);
            if (s4liuyan == null)
            {
                return HttpNotFound();
            }
            return View(s4liuyan);
        }
        public ActionResult talk1(S4liuyan s4liuyan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s4liuyan).State = EntityState.Modified;
                db.SaveChanges();
            }
            return null;
        }
        /// <summary>
        /// 显示留言列表的方法
        /// </summary>
        /// <returns></returns>
        public ActionResult liuyan(int page = 1, int rows = 0)
        {
            var f1 = Session["dname"].ToString();
            var a1 = db.S4doctors.Where(f2 => f2.dname == f1).First();
            int id = a1.did;
            var li = db.S4liuyan.Where(f3 => f3.did == id).ToList();
            List<liuinfo> liu = new List<liuinfo>();
            foreach (var item in li)
            {
                liuinfo lii = new liuinfo();
                lii.lid = item.lid;
                lii.uid = Convert.ToInt32(item.uid);
                lii.uname = item.S4users.uname;
                lii.utalk = item.utalk;
                lii.dtalk = item.dtalk;
                lii.did = Convert.ToInt32( item.did);
                liu.Add(lii);
            }
            ////分页
            int total = liu.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    liu = liu.Take(rows).ToList();
                }
                else
                {
                    liu = liu.Skip((page - 1) * rows).Take(rows).ToList();
                }
            }
            var obj = new
            {
                total = total,
                rows = liu.ToArray()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //修改专家信息
        public ActionResult info()
        {
            var name = Session["dname"].ToString();
            var a1 = db.S4doctors.Where(f2 => f2.dname == name).First();
            int id = a1.did;
            var e = db.S4doctors.Find(id);
            return View(e);
        }



        public ActionResult InfoList(S4doctors d)
        {
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();
            return null;
        }

        //审核
        public ActionResult check()
        {
            return View();
        }

        [HttpPost]
        public ActionResult check(S4orders s4orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s4orders).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.did = new SelectList(db.S4doctors, "did", "dname", s4orders.did);
            ViewBag.otid = new SelectList(db.S4ordertype, "otid", "otname", s4orders.otid);
            ViewBag.uid = new SelectList(db.S4users, "uid", "uname", s4orders.uid);
            ViewBag.tiid = new SelectList(db.timetype, "tiid", "tiname", s4orders.tiid);
            return View(s4orders);
        }

        //审核弹出框
        public ActionResult check2(int ttid)
        {
            S4orders s4orders = db.S4orders.Find(ttid);
            if (s4orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.did = new SelectList(db.S4doctors, "did", "dname", s4orders.did);
            ViewBag.otid = new SelectList(db.S4ordertype, "otid", "otname", s4orders.otid);
            ViewBag.uid = new SelectList(db.S4users, "uid", "uname", s4orders.uid);
            ViewBag.tiid = new SelectList(db.timetype, "tiid", "tiname", s4orders.tiid);
            return View(s4orders);
        }
        //保存
        public ActionResult check3(S4orders p)
        {
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
            return Content("ok");

        }

        //释放数据库上下文对象
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
