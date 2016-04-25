using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NewCyclone.DataBase;

namespace NewCyclone.Models
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Roles {
        /// <summary>
        /// 系统全部角色
        /// </summary>
        public string[] RolesList = { "admin", "user", "member", "guest" };
    }

    /// <summary>
    /// 基类用户
    /// </summary>
    public abstract class SysUser
    {
        /// <summary>
        /// 登录名（ID）
        /// </summary>
        public string loginName { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string[] role { get; set; }


        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime createdOn { get; set; }


        /// <summary>
        /// 是否已经禁用
        /// </summary>
        public bool isDisabled { get; set; }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns>返回被删除的用户对象</returns>
        public SysUser delete() {
            return this;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public abstract void getInfo();

        /// <summary>
        /// 保存用户信息
        /// </summary>
        public abstract void saveInfo();
    }

    /// <summary>
    /// 系统管后台用户
    /// </summary>
    public class SysManagerUser : SysUser
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string fullName;
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobilePhone;
        /// <summary>
        /// 职位
        /// </summary>
        public string jobTitle;
        
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public override void getInfo()
        {
            using (var db = new SysModelContainer())
            {

            }
            throw new SysException("自定义的系统错误", SysExceptionType.系统未能找到匹配的信息);
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        public override void saveInfo()
        {
            string s = "hello";
            int i = int.Parse(s);
        }
    }
}