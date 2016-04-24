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

        [HttpGet]
        public List<SysLog> testDb() {
            SysContext db = new SysContext();
            var r = (from x in db.sysLogs
                                   select new 
                                   {
                                       id = x.id,
                                       message = x.message
                                   }).ToList();
            List<SysLog> result = new List<SysLog>();
            foreach (var s in r) {
                result.Add(new SysLog()
                {
                    id = s.id,
                    message = s.message
                });
            }
            return result;
        }
    }
}
