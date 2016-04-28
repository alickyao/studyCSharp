using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewCyclone.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace NewCyclone.Controllers
{

    /// <summary>
    /// 系统管理员相关
    /// </summary>
    public class ApiSysManagerUserController : ApiController
    {

        /// <summary>
        /// 获取系统角色信息
        /// </summary>
        /// <param name="t">角色的类型</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public List<SysRoles> getRolsList(SysRolesType t) {
            return SysRoles.getRolesList(t);
        }

        /// <summary>
        /// 验证并登陆
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public BaseResponse<SysManagerUser> login(ViewModelLoginReqeust condtion) {
            BaseResponse<SysManagerUser> result = new BaseResponse<SysManagerUser>();
            try
            {
                SysManagerUser smu = new SysManagerUser();
                result.result = smu.checkLogin(condtion);
                FormsAuthentication.SetAuthCookie(result.result.loginName, true);
            }
            catch (SysException ex)
            {
                result = ex.getresult(result, true);
            }
            catch (Exception e) {
                result = SysException.getResult(result, e, condtion);
            }
            return result;
        }

        /// <summary>
        /// 创建管理员
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        [Authorize(Roles ="admin")]
        public BaseResponse<SysManagerUser> createUser(ViewModelUserRegisterRequest condtion) {
            BaseResponse<SysManagerUser> result = new BaseResponse<SysManagerUser>();
            SysManagerUser smu = new SysManagerUser();
            try
            {
                result.result = smu.create(condtion);
            }
            catch (SysException r)
            {
                return r.getresult(result,true);
            }
            catch (Exception e) {
                return SysException.getResult(result, e, condtion);
            }
            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="loginName">用户登录名</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public BaseResponse delete(string loginName)
        {
            BaseResponse res = new BaseResponse();
            try
            {
                SysUser user = new SysManagerUser(loginName);
                user.delete();
            }
            catch (Exception e) {
                res = SysException.getResult(res, e, loginName);
            }
            return res;
        }
    }
}
