//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Health.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class S4doctors
    {
        public S4doctors()
        {
            this.S4liuyan = new HashSet<S4liuyan>();
            this.S4orders = new HashSet<S4orders>();
            this.S4users = new HashSet<S4users>();
        }
    
        public int did { get; set; }
        public string dname { get; set; }
        public string dpwd { get; set; }
        public string dzhicheng { get; set; }
        public string daddress { get; set; }
        public string djianjie { get; set; }
        public string dxiangqing { get; set; }
        public string dapic { get; set; }
        public string dxpic { get; set; }
        public string dsex { get; set; }
        public string shu { get; set; }
        public Nullable<int> tiid { get; set; }
        public Nullable<int> xid { get; set; }
    
        public virtual S4xiangmu S4xiangmu { get; set; }
        public virtual timetype timetype { get; set; }
        public virtual ICollection<S4liuyan> S4liuyan { get; set; }
        public virtual ICollection<S4orders> S4orders { get; set; }
        public virtual ICollection<S4users> S4users { get; set; }
    }
}
