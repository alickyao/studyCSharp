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
        public BaseResponse<List<string>> test()
        {
            BaseResponse<List<string>> r = new BaseResponse<List<string>>();
            return r;
        }
    }
}
