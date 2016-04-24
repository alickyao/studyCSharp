using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewCyclone.Models
{
    /// <summary>
    /// 系统自定义的错误
    /// </summary>
    public class SysException : Exception
    {
        /// <summary>
        /// 系统自定义的错误类别
        /// </summary>
        public SysExceptionType code { get; set; }

        /// <summary>
        /// 错误文本信息
        /// </summary>
        private string message { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        private string condtion { get; set; }

        /// <summary>
        /// 引发异常的应用程序或者对象的名称
        /// </summary>
        private string source { get; set; }

        /// <summary>
        /// 引发错误的堆栈信息
        /// </summary>
        private string stackTrace { get; set; }

        /// <summary>
        /// 引发当前异常的方法
        /// </summary>
        private string targetSite { get; set; }

        /// <summary>
        /// 构造自定义的异常，并保存到系统日志
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="code">自定义的异常类型</param>
        /// <param name="condtion">导致错误的请求参数</param>
        public SysException(string message, SysExceptionType code, object condtion = null) : base()
        {
            this.message = message;
            this.code = code;
        }

        public SysException(Exception e, object condtion = null) : base()
        {
            this.message = e.Message;
            this.code = SysExceptionType.发生其他系统异常;
        }

        /// <summary>
        /// 保存异常消息
        /// </summary>
        /// <param name="type"></param>
        public void saveException(SysMessageType type) {
            this.source = this.Source;
            this.targetSite = this.TargetSite.ToString();
            this.stackTrace = this.StackTrace;

        }

        /// <summary>
        /// 默认返回错误信息的文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.message;
        }
    }

    /// <summary>
    /// 错误类型
    /// </summary>
    public enum SysExceptionType {
        参数未能通过验证,
        系统未能找到匹配的信息,
        发生其他系统异常
    }
}