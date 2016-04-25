using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewCyclone.DataBase;

namespace NewCyclone.Models
{
    /// <summary>
    /// 系统管理员
    /// </summary>
    public class SysManagerUser : SysUser
    {
        public override void getInfo()
        {
            using (var db = new SysModelContainer()) {
                
                
            }
            throw new SysException("自定义的系统错误", SysExceptionType.系统未能找到匹配的信息);
        }

        public override void saveInfo()
        {
            string s = "hello";
            int i = int.Parse(s);
        }
    }
}