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
        /// <summary>
        /// 信息
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createdOn { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public SysMessageType msgType { get; set; }

        /// <summary>
        /// 使用ID构造消息
        /// </summary>
        /// <param name="id"></param>
        public SysMsg(long id) {
            using (var db = new SysModelContainer()) {
                var d = db.Db_SysMsgSet.Single(p => p.Id == id);
                this.Id = d.Id;
                this.createdOn = d.createdOn;
                this.message = d.message;
                this.msgType = (SysMessageType)d.msgType;
            }
        }
    }

    /// <summary>
    /// 用户日志
    /// </summary>
    public class SysUserLog : SysMsg
    {

        /// <summary>
        /// 对应用户的登录名
        /// </summary>
        protected string loginName { get; set; }

        /// <summary>
        /// 对应用户的显示信息
        /// </summary>
        protected SysUser user { get; set; }

        /// <summary>
        /// 对应用户的信息
        /// </summary>
        public string userInfo { get; set; }

        /// <summary>
        /// 用户日志类型
        /// </summary>
        public SysUserLogType logType { get; set; }

        /// <summary>
        /// 关联的其他信息
        /// </summary>
        protected string fkId { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        /// 设备信息
        /// </summary>
        public string device { get; set; }

        /// <summary>
        /// 使用ID构造消息
        /// </summary>
        /// <param name="id"></param>
        public SysUserLog(long id) : base(id)
        {
            using (var db = new SysModelContainer()) {
                var d = db.Db_SysMsgSet.OfType<Db_SysUserLog>().Single(p => p.Id == id);
                this.device = d.device;
                this.ip = d.ip;
                this.fkId = d.fkId;
                this.logType = (SysUserLogType)d.logType;
                this.loginName = d.Db_SysUser_loginName;
            }
            //所属用户判断
            //根据角色来判断
            SysUser user = new SysUser(this.loginName);
            SysRoles userrole = SysRoles.sysRoles.Single(p => p.role.Equals(user.role));
            if (userrole.cat == 0)
            {
                //后台
                this.user = new SysManagerUser(user.loginName);
                this.userInfo = this.user.ToString();
            }
            else {
                //其他,会员
                
            }
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

        /// <summary>
        /// 检索日志
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public static BaseResponseList<SysUserLog> searchLog(ViewModelMsgSearchUserLogReqeust condtion) {
            BaseResponseList<SysUserLog> result = new BaseResponseList<SysUserLog>();
            using (var db = new SysModelContainer()) {
                var r = (from c in db.Db_SysMsgSet.OfType<Db_SysUserLog>().AsEnumerable()
                         where ((string.IsNullOrEmpty(condtion.loginName) && string.IsNullOrEmpty(condtion.fkid))? true :(
                            (string.IsNullOrEmpty(condtion.loginName)?false:c.Db_SysUser_loginName == condtion.loginName) || (string.IsNullOrEmpty(condtion.fkid) ? false : c.fkId == condtion.fkid)
                         ))
                         && (condtion.logType == null ? true : c.logType == condtion.logType.GetHashCode())
                         orderby c.createdOn descending
                         select c.Id);
                result.total = r.Count();
                if (result.total > 0) {
                    if (condtion.page == 0)
                    {
                        result.rows = r.Select(p => new SysUserLog(p)).ToList();
                    }
                    else {
                        result.rows = r.Skip(condtion.getSkip()).Take(condtion.pageSize).Select(p => new SysUserLog(p)).ToList();
                    }
                }
            }
            return result;
        }
    }

    /// <summary>
    /// 用户日志检索请求
    /// </summary>
    public class ViewModelMsgSearchUserLogReqeust : BaseRequest {

        /// <summary>
        /// 用户ID，登录名
        /// </summary>
        public string loginName { get; set; }

        /// <summary>
        /// 关联的ID
        /// </summary>
        public string fkid { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public Nullable<SysUserLogType> logType { get; set; }
    }
}