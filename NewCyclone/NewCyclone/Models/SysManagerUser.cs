using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewCyclone.Models
{
    /// <summary>
    /// 系统管理员
    /// </summary>
    public class SysManagerUser : SysUser
    {
        public override void getInfo()
        {
            throw new SysException("自定义的系统错误", SysExceptionType.系统未能找到匹配的信息);
        }

        public override void saveInfo()
        {
            try
            {
                string s = "hello";
                int i = int.Parse(s);
            }
            catch (Exception e) {
                throw new SysException(e);
            }
        }
    }
}