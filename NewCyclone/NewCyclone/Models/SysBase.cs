using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewCyclone.Models
{
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
    /// 基本返回类型
    /// </summary>
    public enum BaseResponse {
        /// <summary>
        /// 操作成功
        /// </summary>
        正常,
        /// <summary>
        /// 发生异常
        /// </summary>
        异常
    }

    /// <summary>
    /// 标准返回对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T> {

        /// <summary>
        /// 代码
        /// </summary>
        public BaseResponse code { get; set; }

        /// <summary>
        /// 返回提示
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 具体的返回内容
        /// </summary>
        public T result { get; set; }
    }
}