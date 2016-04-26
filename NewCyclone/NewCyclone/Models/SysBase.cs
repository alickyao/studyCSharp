using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public enum BaseResponseCode {
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
    public class BaseResponse {
        /// <summary>
        /// 代码
        /// </summary>
        public BaseResponseCode code { get; set; }

        /// <summary>
        /// 返回提示
        /// </summary>
        public string msg { get; set; }
    }

    /// <summary>
    /// 带数据的标准返回对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T> {

        /// <summary>
        /// 代码
        /// </summary>
        public BaseResponseCode code { get; set; }

        /// <summary>
        /// 返回提示
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 具体的返回内容
        /// </summary>
        public T result { get; set; }
    }

    /// <summary>
    /// 系统验证
    /// </summary>
    public class SysValidata {
        /// <summary>
        /// 参数验证
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public static BaseResponse<List<ValidationResult>> valiData(object condtion) {
            BaseResponse<List<ValidationResult>> result = new BaseResponse<List<ValidationResult>>();

            var context = new ValidationContext(condtion, null, null);

            var results = new List<ValidationResult>();
            Validator.TryValidateObject(condtion, context, results, true);

            result.result = results;

            if (results != null && results.Count > 0) {
                result.code = BaseResponseCode.异常;
                result.msg = results[0].ErrorMessage;
            }
            return result;
        }
    }
}