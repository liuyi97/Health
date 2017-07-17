using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Models;


namespace Health.Controllers.dental
{
    public class mingpiaoController : Controller
    {
        //
        // GET: /mingpiao/

        S4HealthEntities db = new S4HealthEntities();
        public ActionResult Index()
        {
            return View();
        }
        //首页
        public ActionResult shouye()
        {
            IEnumerable<S4machine> ma = db.S4machine.ToList();
            IEnumerable<S4huanjing> hu = db.S4huanjing.ToList();
            IEnumerable<S4doctors> doo = db.S4doctors.ToList();
            var jg = (from m in ma
                      join h in hu on m.maid equals h.hjid
                      from c in doo
                      where c.did == m.maid 
                      select new { m.maid, m.maimage, m.maname, h.hjid, h.hjname, h.hjimage, c.dapic, c.did }).ToList();
            //var jg = (from m in ma join h in hu on m.maid equals h.hjid select new { m.maid,m.maimage,m.maname,h.hjid,h.hjname,h.hjimage }).ToList();
            List<sbhj> ml = new List<sbhj>();
            foreach (var item in jg)
            {
                sbhj sb = new sbhj();
                sb.hjimage = item.hjimage;
                sb.hjname = item.hjname;
                sb.maimage = item.maimage;
                sb.maname = item.maname;
                sb.did = item.did;
                sb.dapic = item.dapic;
                ml.Add(sb);
            }
            return View(ml);
        }
        //牙齿种植
        public ActionResult jzxm()
        {
            IEnumerable<S4zhishi> ma = db.S4zhishi.Where(n => n.xid == 1).ToList();
            //List<S4zhishi> l = new List<S4zhishi>();
            //var p = db.S4zhishi.Where(n => n.xid == 1).ToList();  
            //foreach (var item in ma)
            //{
            //    S4zhishi xx = new S4zhishi();
            //    xx.zsid = item.zsid;
            //    xx.xid = item.xid;              
            //    l.Add(xx);
            ////}
            return View(ma);
        }
        //牙齿矫正
        public ActionResult ycjz()
        {
            IEnumerable<S4zhishi> ma = db.S4zhishi.Where(n => n.xid == 2).ToList();
            return View(ma);
        }
        //美容修复
        public ActionResult mrxf()
        {
            IEnumerable<S4zhishi> ma = db.S4zhishi.Where(n => n.xid == 3).ToList();
            return View(ma);

        }
        //牙周治疗
        public ActionResult yzzl()
        {
            IEnumerable<S4zhishi> ma = db.S4zhishi.Where(n => n.xid == 4).ToList();
            return View(ma);
        }
        //综合治疗
        public ActionResult zhzl()
        {
            IEnumerable<S4zhishi> ma = db.S4zhishi.Where(n => n.xid == 5).ToList();
            return View(ma);
        }
        //儿童牙科
        public ActionResult rtyk()
        {
            IEnumerable<S4zhishi> ma = db.S4zhishi.Where(n => n.xid == 6).ToList();
            return View(ma);
        }
        //来院路线
        public ActionResult lylx()
        {
            return View();
        }
        //知识链接
        public ActionResult zslj(int zsid)
        {

            //ViewBag.t = new SelectList(db.S4xiangmu, "xid", "xname");
            var p = db.S4zhishi.Find(zsid);
            return View(p);


        }

        //释放数据库上下文对象
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
