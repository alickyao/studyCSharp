using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewCyclone.Models
{
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
    /// 标准返回列表对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponseList<T> {
        /// <summary>
        /// 存在的记录的总数
        /// </summary>
        public int total { get; set; }


        private List<T> _rows = new List<T>();
        /// <summary>
        /// 行
        /// </summary>
        public List<T> rows {
            get { return _rows; }
            set { _rows = value; }
        }
    }

    /// <summary>
    /// 标准列表请求参数
    /// </summary>
    public class BaseRequest {
        private int _page = 1;
        /// <summary>
        /// 调取的页码 默认1 
        /// </summary>
        public int page {
            get { return _page; }
            set { _page = value; }
        }

        private int _pageSize = 20;
        /// <summary>
        /// 每页的数量 默认 20
        /// </summary>
        public int pageSize {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        /// <summary>
        /// 根据页码获取需要跳过的行的数量
        /// </summary>
        /// <returns></returns>
        public int getSkip() {
            return (this.page - 1) * this.pageSize;
        }
    }

    /// <summary>
    /// 系统验证
    /// </summary>
    public class SysValidata {
        /// <summary>
        /// 参数验证
        /// </summary>
        /// <param name="condtion">参数</param>
        /// <param name="throwException">是否直接抛出异常，默认为true</param>
        /// <returns></returns>
        public static BaseResponse<List<ValidationResult>> valiData(object condtion,bool throwException = true) {
            BaseResponse<List<ValidationResult>> result = new BaseResponse<List<ValidationResult>>();

            var context = new ValidationContext(condtion, null, null);

            var results = new List<ValidationResult>();
            Validator.TryValidateObject(condtion, context, results, true);

            result.result = results;

            if (results != null && results.Count > 0) {
                if (throwException)
                {
                    throw new SysException(results[0].ErrorMessage, condtion);
                }
                result.code = BaseResponseCode.异常;
                result.msg = results[0].ErrorMessage;
            }
            return result;
        }
    }
}