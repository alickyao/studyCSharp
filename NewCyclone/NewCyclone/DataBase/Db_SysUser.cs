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
    
    public abstract partial class Db_SysUser
    {
        public string loginName { get; set; }
        public System.DateTime createdOn { get; set; }
        public bool isDeleted { get; set; }
        public bool isDisabled { get; set; }
        public Nullable<System.DateTime> lastLoginTime { get; set; }
        public string passWord { get; set; }
        public string role { get; set; }
    }
}
