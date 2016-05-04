using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewCyclone.Models;

namespace NewCyclone.Controllers
{
    /// <summary>
    /// 测试用
    /// </summary>
    public class ApiTestController : ApiController
    {
        /// <summary>
        /// 测试
        /// </summary>
        [HttpGet]
        [SysAuthorize(RoleType = SysRolesType.后台)]
        public BaseResponse<List<string>> test()
        {
            BaseResponse<List<string>> r = new BaseResponse<List<string>>();
            r.result = new List<string>();
            foreach (var s in Enum.GetValues(typeof(SysMessageType))) {
                int value = Convert.ToInt32(s);
                string text = s.ToString();
                r.result.Add(value.ToString() + text);
            }
            return r;
        }
    }
}
