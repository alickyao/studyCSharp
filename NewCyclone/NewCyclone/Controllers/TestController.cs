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
            SysManagerUser smu = new SysManagerUser();
            smu.id = "123";
            try
            {
                //smu.getInfo();
                smu.saveInfo();
            }
            catch (SysException ex)
            {
                ex.saveException(SysMessageType.错误);
                return ex.ToString();
            }
            return "";
        }
    }
}
