using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewCyclone.Models;

namespace NewCyclone.Controllers
{
    /// <summary>
    /// 系统消息 日志 错误
    /// </summary>
    public class ApiSysMsgController : ApiController
    {
        /// <summary>
        /// 获取全部消息类型
        /// </summary>
        /// <returns></returns>
        public List<VMComboBox> getMesTypeList() {
            return SysHelp.getEnumList(typeof(SysMessageType));
        }

        /// <summary>
        /// 获取全部用户日志类型
        /// </summary>
        /// <returns></returns>
        public List<VMComboBox> getUserLogTypeList()
        {
            return SysHelp.getEnumList(typeof(SysUserLogType));
        }

        /// <summary>
        /// 检索系统日志
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public BaseResponse<BaseResponseList<SysUserLog>> searchUserLog(ViewModelMsgSearchUserLogReqeust condtion) {
            BaseResponse<BaseResponseList<SysUserLog>> result = new BaseResponse<BaseResponseList<SysUserLog>>();
            try
            {
                result.result = SysUserLog.searchLog(condtion);
            }
            catch (Exception e) {
                SysException.getResult(result, e, condtion);
            }
            return result;
        }
    }
}
