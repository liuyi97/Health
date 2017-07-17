using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.Models
{
    public class ChaUser
    {
        public int uid { get; set; }
        public string uname { get; set; }
        public string uacount { get; set; }
        public string upwd { get; set; }
        public string uphone { get; set; }
        public string img { get; set; }
        public int  did { get; set; }
        public string uscore { get; set; }
        public string dname { get; set; }
        public int otid { get; set; }
        public string otname { get; set; }
    }
}