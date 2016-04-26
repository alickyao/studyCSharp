using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewCyclone.DataBase;
using Newtonsoft.Json;

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
        public string message { get; set; }

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
            if (condtion != null) {
                this.condtion = JsonConvert.SerializeObject(condtion);
            }
        }

        public SysException(Exception e, object condtion = null) : base(e.Message, e)
        {
            this.source = e.Source;
            this.targetSite = e.TargetSite.ToString();
            this.stackTrace = e.StackTrace;

            this.message = e.Message;
            this.code = SysExceptionType.发生其他系统异常;
            if (condtion != null)
            {
                this.condtion = JsonConvert.SerializeObject(condtion);
            }
        }

        /// <summary>
        /// 保存异常消息
        /// </summary>
        /// <param name="type"></param>
        public void saveException() {

            if (!(this.code == SysExceptionType.发生其他系统异常)) {
                this.source = base.Source;
                this.targetSite = base.TargetSite.ToString();
                this.stackTrace = base.StackTrace;
            }

            using (var db = new SysModelContainer()) {
                Db_SysExceptionLog log = new Db_SysExceptionLog()
                {
                    condtion = this.condtion,
                    createdOn = DateTime.Now,
                    errorCode = this.code.GetHashCode(),
                    message = this.message,
                    msgType = SysMessageType.异常.GetHashCode(),
                    source = this.source,
                    stackTrace = this.stackTrace,
                    targetSite = this.targetSite
                };
                db.Db_SysMsgSet.Add(log);
                db.SaveChanges();
            }
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
        /// <summary>
        /// 传入的参数可能不符合要求
        /// </summary>
        参数未能通过验证,
        /// <summary>
        /// 未能在系统中找到对应的信息
        /// </summary>
        系统未能找到匹配的信息,
        /// <summary>
        /// 发送其他异常情况
        /// </summary>
        发生其他系统异常
    }
}