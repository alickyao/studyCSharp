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
    /// 分类树
    /// </summary>
    public class ApiSysCatTreeController : ApiController
    {

        /// <summary>
        /// 添加/编辑分类树
        /// </summary>
        /// <param name="condtion">添加/编辑分类树请求参数</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public BaseResponse<SysCatTree> editCatTree(VMTreeEditCatTreeRequest condtion) {
            BaseResponse<SysCatTree> result = new BaseResponse<SysCatTree>();
            try
            {
                result.result = SysCatTree.edit(condtion);
                result.msg = "保存成功";
            }
            catch (SysException ex)
            {
                result = ex.getresult(result);
            }
            catch (Exception e) {
                result = SysException.getResult(result, e, condtion);
            }
            return result;
        }

        /// <summary>
        /// 根据功能标示获取分类树集合
        /// </summary>
        /// <param name="fun">功能标示符</param>
        /// <returns></returns>
        [HttpGet]
        public BaseResponse<List<SysCatTree>> getTreelist(string fun) {
            BaseResponse<List<SysCatTree>> result = new BaseResponse<List<SysCatTree>>();
            try
            {
                result.result = SysCatTree.getTreeList(fun);
            }
            catch (Exception e) {
                result = SysException.getResult(result, e, fun);
            }
            return result;
        }
    }
}
