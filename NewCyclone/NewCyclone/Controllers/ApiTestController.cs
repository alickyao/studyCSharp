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
        public BaseResponse<SysCatTree> test()
        {
            BaseResponse<SysCatTree> res = new BaseResponse<SysCatTree>();

            res.result = new SysCatTree("xsgj_160506101615", 3);

            return res;
        }
    }
}
