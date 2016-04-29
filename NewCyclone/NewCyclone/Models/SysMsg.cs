using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewCyclone.DataBase;
using System.Security.Principal;

namespace NewCyclone.Models
{

    /// <summary>
    /// 实现该接口的类需提供方法将类的信息格式化为日志文本
    /// </summary>
    public interface ItoSysLogMesable {
        /// <summary>
        /// 返回日志文本
        /// </summary>
        /// <returns></returns>
        string toLogString();
    }

    /// <summary>
    /// 系统消息类型
    /// </summary>
    public enum SysMessageType
    {
        /// <summary>
        /// 来自远程服务器的通知
        /// </summary>
        通知,
        /// <summary>
        /// 用户日志
        /// </summary>
        日志,
        /// <summary>
        /// 异常信息
        /// </summary>
        异常
    }

    /// <summary>
    /// 用户日志类型
    /// </summary>
    public enum SysUserLogType {
        注册,
        登陆,
        编辑,
        删除,
    }


    /// <summary>
    /// 系统消息
    /// </summary>
    public class SysMsg 
    {
        public SysMsg() {
        }
    }

    /// <summary>
    /// 用户日志
    /// </summary>
    public class SysUserLog : SysMsg
    {

        public SysUserLog() : base() {

        }

        /// <summary>
        /// 保存系统用户日志
        /// </summary>
        /// <param name="condtion">需要保存的参数</param>
        /// <param name="t">类型</param>
        /// <param name="fkId">关联的ID</param>
        public static void saveLog(ItoSysLogMesable condtion, SysUserLogType t, string fkId = null)
        {
            saveLog(condtion.toLogString(), t, fkId);
        }


        /// <summary>
        /// 保存系统用户日志
        /// </summary>
        /// <param name="message">需要保存的日志文本</param>
        /// <param name="t">类型</param>
        /// <param name="fkId">关联的ID</param>
        public static void saveLog(string message, SysUserLogType t, string fkId = null)
        {
            string loginname = "admin";
            IIdentity user = HttpContext.Current.User.Identity;
            if (user.IsAuthenticated) {
                loginname = user.Name;
            }
            using (var db = new SysModelContainer())
            {
                Db_SysUserLog log = new Db_SysUserLog()
                {
                    createdOn = DateTime.Now,
                    Db_SysUser_loginName = loginname,
                    fkId = fkId,
                    logType = t.GetHashCode(),
                    msgType = SysMessageType.日志.GetHashCode(),
                    message = message,
                    ip = HttpContext.Current.Request.UserHostAddress,
                    device =HttpContext.Current.Request.UserAgent
                };
                db.Db_SysMsgSet.Add(log);
                db.SaveChanges();
            }
        }
    }
}