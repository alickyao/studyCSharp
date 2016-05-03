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
            for (int i = 10000; i < 20000; i++) {
                SysManagerUser user = SysManagerUser.create(new ViewModelUserRegisterRequest() {
                    fullName = "测试" + i,
                    loginname = "text" + i,
                    role = "user",
                    mobilePhone = ""
                });
                r.result.Add(user.loginName);
            }
            return r;
        }
    }
}
