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
        public string[] getRolsList() {
            return SysRoles.RolesList;
        }

        /// <summary>
        /// 创建管理员
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public BaseResponse<SysManagerUser> createUser(ViewModelUserRegisterRequest condtion) {
            BaseResponse<SysManagerUser> result = new BaseResponse<SysManagerUser>();
            
            var v = SysValidata.valiData(condtion);
            if (v.code == BaseResponseCode.正常)
            {
                try
                {
                    SysManagerUser smu = new SysManagerUser();
                    result.result = smu.create(condtion);
                    return result;
                }
                catch (SysException ex)
                {
                    ex.saveException();
                    result.code = BaseResponseCode.异常;
                    result.msg = ex.message;
                    return result;
                }
                catch (Exception e)
                {
                    SysException ex = new SysException(e, condtion);
                    ex.saveException();
                    result.code = BaseResponseCode.异常;
                    result.msg = ex.message;
                    return result;
                }
            }
            else {
                return new BaseResponse<SysManagerUser>()
                {
                    code = v.code,
                    msg = v.msg
                };
            }
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
            SysUser user = new SysManagerUser(loginName);
            user.delete();
            return res;
        }
    }
}
