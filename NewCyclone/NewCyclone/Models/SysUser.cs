using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace NewCyclone.Models
{
    /// <summary>
    /// 基类用户
    /// </summary>
    public abstract class SysUser
    {
        /// <summary>
        /// 用户的ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public SysRole[] role { get; set; }

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
}