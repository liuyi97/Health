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
    
    public partial class S4anli
    {
        public int liid { get; set; }
        public Nullable<int> uid { get; set; }
        public Nullable<int> xid { get; set; }
        public string liqian { get; set; }
        public string lihou { get; set; }
        public string ajieshao { get; set; }
    
        public virtual S4users S4users { get; set; }
        public virtual S4xiangmu S4xiangmu { get; set; }
    }
}
