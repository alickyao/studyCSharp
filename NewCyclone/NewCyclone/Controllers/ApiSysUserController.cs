using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewCyclone.Models;
using System.ComponentModel.DataAnnotations;

namespace NewCyclone.Controllers
{

    /// <summary>
    /// 系统管理员相关
    /// </summary>
    public class ApiSysManagerUserController: ApiController
    {
        /// <summary>
        /// 获取系统角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<SysRoles> getRolsList() {
            return SysRoles.sysRoles;
        }

        /// <summary>
        /// 创建管理员
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public BaseResponse<SysManagerUser> createUser(ViewModelUserRegisterRequest condtion) {
            BaseResponse<SysManagerUser> result = new BaseResponse<SysManagerUser>();
            SysManagerUser smu = new SysManagerUser();
            try
            {
                result.result = smu.create(condtion);
            }
            catch (SysException r)
            {
                r.save();
                return r.getresult(result);
            }
            catch (Exception e) {
                SysException.save(e, condtion);
                return SysException.getResult(result);
            }
            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="loginName">用户登录名</param>
        /// <returns></returns>
        [HttpGet]
        public BaseResponse delete(string loginName)
        {
            BaseResponse res = new BaseResponse();
            try
            {
                SysUser user = new SysManagerUser(loginName);
                user.delete();
            }
            catch (Exception e) {
                SysException.save(e, loginName);
                res = SysException.getResult(res);
            }
            return res;
        }
    }
}
