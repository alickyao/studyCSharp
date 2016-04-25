using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewCyclone.Models;

namespace NewCyclone.Controllers
{
    public class TestController : ApiController
    {
        /// <summary>
        /// 测试系统错误
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string testException()
        {
            try
            {
                SysUser user = new SysManagerUser();
                user.getInfo();
                return "ok";
            }
            catch (SysException ex)
            {
                ex.saveException(SysMessageType.异常);
                return ex.ToString();
            }
            catch (Exception e)
            {
                SysException ex = new SysException(e);
                ex.saveException(SysMessageType.异常);
                return ex.ToString();
            }
        }
    }
}
