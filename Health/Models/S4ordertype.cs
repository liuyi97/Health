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
    
    public partial class S4ordertype
    {
        public S4ordertype()
        {
            this.S4orders = new HashSet<S4orders>();
        }
    
        public int otid { get; set; }
        public string otname { get; set; }
    
        public virtual ICollection<S4orders> S4orders { get; set; }
    }
}
