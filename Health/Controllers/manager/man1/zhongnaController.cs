
using Health.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.Controllers.dental
{

    public class zhongnaController : Controller
    {
        //
        // GET: /zhongna/
        //数据库上下文对象  
        S4HealthEntities hen = new S4HealthEntities();

        //头部和尾部
        public ActionResult Index()
        {
            return View();
        }
        //后台
        public ActionResult guanliyuan(string q)
        {
            var a = q;
            ViewBag.mm = a;
            return View();
        }
        /// <summary>
        /// 用户的查询
        /// </summary>
        /// <returns></returns>
        public ActionResult yonghu()
        {
            return View();
        }
        public ActionResult Edityh(string name, int tt, string zhanghao, string mima, string phone, string img)
        {
            var mm = hen.S4users.Find(tt);
            mm.uname = name;
            mm.uacount = zhanghao;
            mm.upwd = mima;
            mm.uphone = phone;
            mm.img = img;
            hen.SaveChanges();
            return null;
        }
        public ActionResult cyh(string uname, int page = 1, int rows = 10)
        {
            var list = hen.S4orders.ToList();
            List<ChaUser> cha = new List<ChaUser>();
            foreach (var item in list)
            {
                ChaUser c = new ChaUser();
                c.uid = item.S4users.uid;
                c.uname = item.S4users.uname;
                c.uacount = item.S4users.uacount;
                c.upwd = item.S4users.upwd;
                c.uphone = item.S4users.uphone;
                c.img = item.S4users.img;
                c.uscore = item.S4users.uscore;
                //c.did = Convert.ToInt32(item.did);
                c.dname = item.S4doctors.dname;
                c.otname = item.S4ordertype.otname;
                cha.Add(c);
            }
            if (uname != "" && uname != null)
            {
                cha = cha.Where(n => n.uname.Contains(uname)).ToList();
            }
            int total = cha.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    cha = cha.Take(rows).ToList();
                }
                else
                {
                    cha = cha.Skip((page - 1) * rows).Take(rows).ToList();
                }
            }
            var obj4 = new
            {
                total = total,
                rows = cha.ToArray()

            };
            return Json(obj4, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 管理员的增删改
        /// </summary>
        /// <returns></returns>
        public ActionResult guanliyuan2()
        {
            return View();
        }
        public ActionResult cgly()
        {
            var list = hen.S4manager.ToList();         
            List<S4manager> mm = new List<S4manager>();
            foreach (var item in list)
            {
                S4manager man = new S4manager();
                man.mid = item.mid;
                man.mname = item.mname;
                man.macount = item.macount;
                man.mpwd = item.mpwd;
                mm.Add(man);
            }
         
            return Json(mm, JsonRequestBehavior.AllowGet);
        }
             
        //添加管理员信息
        public ActionResult AddManager()
        {

            return View();
        }
        public ActionResult AddManager2(S4manager m)
        {
            hen.S4manager.Add(m);
            hen.SaveChanges();
            return View();
        }
        //修改管理员信息
        public ActionResult Edit(int? mid)
        {

            S4manager m = hen.S4manager.Find(mid);
            hen.SaveChanges();
            return View(m);
        }
        public ActionResult Editgly(S4manager m)
        {
            hen.Entry(m).State = EntityState.Modified;
            hen.SaveChanges();
            return null;
        }
        //删除管理员信息
        public ActionResult DelManager(int mid)
        {
            S4manager m = hen.S4manager.Find(mid);
            hen.S4manager.Remove(m);
            hen.SaveChanges();
            return null;

          
        }
        /// <summary>
        /// 专家的增删改查
        /// </summary>
        /// <returns></returns>
        public ActionResult zhuanjia()
        {
            return View();
        }
        //显示专家列表和查询
        public ActionResult czj(string dname, int page = 1, int rows = 10)
        {
            var list = hen.S4doctors.ToList();
            List<ChaDoc> doc = new List<ChaDoc>();
            foreach (var item in list)
            {
                ChaDoc d = new ChaDoc();
                d.did = item.did;
                d.dname = item.dname;
                d.dpwd = item.dpwd;
                d.dzhicheng = item.dzhicheng;
                d.daddress = item.daddress;
                d.djianjie = item.djianjie;
                d.dxiangqing = item.dxiangqing;
                d.dapic = item.dapic;
                d.dxpic = item.dxpic;
                d.dsex = item.dsex;
                d.shu = item.shu;
                d.tiid = Convert.ToInt32(item.tiid);
                d.tiname = item.timetype.tiname;
                d.xname = item.S4xiangmu.xname;
                d.xid = item.S4xiangmu.xid;
                //d.time = item.time;
                doc.Add(d);
            }
            if (dname != "" && dname != null)
            {
                doc = doc.Where(n => n.dname.Contains(dname)).ToList();
            }
            int total = doc.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    doc = doc.Take(rows).ToList();
                }
                else
                {
                    doc = doc.Skip((page - 1) * rows).Take(rows).ToList();
                }
            }
            var obj = new
            {
                total = total,
                rows = doc.ToArray()

            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        //添加专家信息
        public ActionResult Addzj()
        {
            ViewBag.mm = new SelectList(hen.timetype.ToList(), "tiid", "tiname");
            ViewBag.mmm = new SelectList(hen.S4xiangmu.ToList(), "xid", "xname");
            return View();
        }
        public ActionResult Addzj2(S4doctors docs)
        {
            hen.S4doctors.Add(docs);
            hen.SaveChanges();
            return View();
        }
        //修改专家信息
        public ActionResult Editzj(int did)
        {
            S4doctors d = hen.S4doctors.Find(did);
            ViewBag.mm = new SelectList(hen.timetype.ToList(), "tiid", "tiname");
            ViewBag.mmm = new SelectList(hen.S4xiangmu.ToList(), "xid", "xname");
            //hen.SaveChanges();
            return View(d);
        }
        public ActionResult Editzj2(S4doctors doc)
        {
            hen.Entry(doc).State = EntityState.Modified;
            hen.SaveChanges();
            return null;
        }
        //删除专家信息
        public ActionResult Delzj(int did)
        {
            S4doctors doc = hen.S4doctors.Find(did);
            hen.S4doctors.Remove(doc);
            hen.SaveChanges();
            return null;
        }
        /// <summary>
        /// 新闻的增删改查
        /// </summary>
        /// <returns></returns>
        public ActionResult xinwen()
        {
            ViewBag.mm = new SelectList(hen.S4newstype.ToList(), "ntid", "ntname");
            return View();
        }
        public ActionResult cxw(string mm, int page = 1, int rows = 10)
        {
            var list = hen.S4news.ToList();
            List<ChaNews> cha = new List<ChaNews>();
            foreach (var item in list)
            {
                ChaNews c = new ChaNews();
                c.nid = item.nid;
                c.ninfo = item.ninfo;
                c.nname = item.nname;
                c.ntname = item.S4newstype.ntname;
                c.pic = item.pic;
                c.ntid = Convert.ToInt32(item.ntid);
                cha.Add(c);
            }
            if (mm != "" && mm != null)
            {
                int m = Convert.ToInt32(mm);
                cha = cha.Where(n => n.ntid == m).ToList();
            }
            int total = cha.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    cha = cha.Take(rows).ToList();
                }
                else
                {
                    cha = cha.Skip((page - 1) * rows).Take(rows).ToList();
                }
            }
            var obj2 = new
            {
                total = total,
                rows = cha.ToArray()

            };
            return Json(obj2, JsonRequestBehavior.AllowGet);
        }
        //添加新闻
        public ActionResult AddNews()
        {

            ViewBag.mm = new SelectList(hen.S4newstype.ToList(), "ntid", "ntname");
            return View();
        }
        public ActionResult AddNews2(S4news news)
        {
            hen.S4news.Add(news);
            hen.SaveChanges();
            return View();
        }

        //删除新闻
        public ActionResult DelNews(int nid)
        {
            S4news n = hen.S4news.Find(nid);
            hen.S4news.Remove(n);
            hen.SaveChanges();
            return null;

        }
        //修改
        public ActionResult EditNews(int nid)
        {
            S4news n = hen.S4news.Find(nid);
            ViewBag.mm = new SelectList(hen.S4newstype.ToList(), "ntid", "ntname");
            return View(n);
        }
        public ActionResult EditNews2(S4news news)
        {
            hen.Entry(news).State = EntityState.Modified;
            hen.SaveChanges();
            return null;
        }
        /// <summary>
        /// 留言的显示
        /// </summary>
        /// <returns></returns>
        public ActionResult liuyan()
        {
            return View();
        }
        public ActionResult cly(int page=1,int rows=10)
        {
            var list = hen.S4liuyan.ToList();
            List<ChaLiuyan> liu = new List<ChaLiuyan>();

            foreach (var item in list)
            {
                ChaLiuyan l = new ChaLiuyan();
                l.lid = item.lid;
                l.utalk = item.utalk;
                l.dtalk = item.dtalk;
                l.dname = item.S4doctors.dname;
                l.uname = item.S4users.uname;
                liu.Add(l);
            }
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
            var lll = new
            {
                total = total,
                rows = liu.ToArray()

            };
            return Json(lll, JsonRequestBehavior.AllowGet);
        }       
       
        /// <summary>
        /// 测试题的增删改
        /// </summary>
        /// <returns></returns>
        public ActionResult ceshi()
        {
            return View();
        }
        //显示
        public ActionResult ccs(int page = 1, int rows = 10)
        {
            var list = hen.S4text.ToList();
            List<S4text> text = new List<S4text>();
            foreach (var item in list)
            {
                S4text t = new S4text();
                t.tid = item.tid;
                t.tabout = item.tabout;
                t.tscore = item.tscore;
                text.Add(t);
            }
            int total = text.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    text = text.Take(rows).ToList();
                }
                else
                {
                    text = text.Skip((page - 1) * rows).Take(rows).ToList();
                }
            }
            var obj3 = new
            {
                total = total,
                rows = text.ToArray()

            };
            return Json(obj3, JsonRequestBehavior.AllowGet);
        }
        //添加测试题
        public ActionResult Addcs()
        {
            return View();
        }
        public ActionResult Addcs2(S4text t)
        {
            hen.S4text.Add(t);
            hen.SaveChanges();
            return View();
        }
        //修改测试题
        public ActionResult Editcs(int tid)
        {
            S4text t = hen.S4text.Find(tid);
            return View(t);
        }
        public ActionResult Editcs2(S4text t)
        {
            hen.Entry(t).State = EntityState.Modified;
            hen.SaveChanges();
            return null;
        }
        //删除测试题
        public ActionResult Delcs(int tid)
        {
            S4text t = hen.S4text.Find(tid);
            hen.S4text.Remove(t);
            hen.SaveChanges();
            return null;
        }
        /// <summary>
        /// 项目的增删改
        /// </summary>
        /// <returns></returns>
        public ActionResult xiangmu()
        {
            return View();
        }
        //显示项目
        public ActionResult cxm()
        {
            var list = hen.S4xiangmu.ToList();
            List<S4xiangmu> xm = new List<S4xiangmu>();
            foreach (var item in list)
            {
                S4xiangmu x = new S4xiangmu();
                x.xid = item.xid;
                x.xjieshao = item.xjieshao;
                x.xname = item.xname;
                xm.Add(x);
            }
            return Json(xm, JsonRequestBehavior.AllowGet);
        }
        //添加项目
        
        //删除项目
        public ActionResult Delxm(int xid)
        {
            S4xiangmu x = hen.S4xiangmu.Find(xid);
            hen.S4xiangmu.Remove(x);
            hen.SaveChanges();
            return null;
        }
        /// <summary>
        /// 成功案例的查询
        /// </summary>
        /// <returns></returns>
      
        public ActionResult Success()
        {
           
            return View();
        }

        //显示成功案例
        public ActionResult csucc()
        {
            var list = hen.S4anli.ToList();
            List<ChaSuccess> suc = new List<ChaSuccess>();
            foreach (var item in list)
            {
                ChaSuccess s = new ChaSuccess();
                s.liid = item.liid;
                s.liqian = item.liqian;
                s.lihou = item.lihou;
                s.uname = item.S4users.uname;
                s.xname = item.S4xiangmu.xname;
                s.xjieshao = item.S4xiangmu.xjieshao;
                suc.Add(s);
            }
            return Json(suc, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        public ActionResult People(int? p)
        {
            var mm = hen.S4users.Where(m => m.uid == p).First();
            ViewBag.m1 = mm.uid;
            ViewBag.m2 = mm.uname;
            ViewBag.m3 = mm.uphone;
            ViewBag.m4 = mm.upwd;
            ViewBag.m5 = mm.uacount;
            ViewBag.m6 = mm.img;        
            return View();
        }
     
        public ActionResult Addly(string name1, string phone1, string dname1, string neirong1)
        {
            var mm = hen.S4doctors.Where(m => m.dname == dname1).First();
            var nn = hen.S4users.Where(m=>m.uname==name1).First();         
            S4liuyan ly = new S4liuyan();
            ly.did = mm.did;
            ly.uid = nn.uid;
            ly.utalk = neirong1;
            nn.uphone = phone1;
            nn.uname = name1;
            mm.dname = dname1;
            hen.S4liuyan.Add(ly);
            hen.SaveChanges();
            return View();
        }
        //释放数据库上下文
        protected override void Dispose(bool disposing)
        {
            hen.Dispose();
            base.Dispose(disposing);
        }

    }
}

