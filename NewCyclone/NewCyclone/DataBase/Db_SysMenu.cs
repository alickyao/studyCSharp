//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewCyclone.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Db_SysMenu : Db_SysTree
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Db_SysMenu()
        {
            this.Db_SysRole = new HashSet<Db_SysRole>();
        }
    
        public string url { get; set; }
        public string icon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Db_SysRole> Db_SysRole { get; set; }
    }
}
