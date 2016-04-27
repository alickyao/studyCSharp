using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}