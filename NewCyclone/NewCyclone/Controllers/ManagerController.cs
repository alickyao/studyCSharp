﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using NewCyclone.Models;

namespace NewCyclone.Controllers
{
    /// <summary>
    /// 后台基础
    /// </summary>
    public class MBaseController : Controller {
        /// <summary>
        /// 页面的ID
        /// </summary>
        public string pageId;
        /// <summary>
        /// 为页面ID赋值
        /// </summary>
        public MBaseController() {
            pageId = SysHelp.getNewId("HHmmss");
            ViewBag.pageId = pageId;
        }

        /// <summary>
        /// 重新设置页面的PageId
        /// </summary>
        /// <param name="pId">重设的pageId</param>
        public void setPageId(string pId = null) {
            if (!string.IsNullOrEmpty(pId)) {
                this.pageId = pId;
                ViewBag.pageId = pId;
            }
        }
    }
    /// <summary>
    /// 后台系统界面控制登陆退出与主界面
    /// </summary>
    public class ManagerController : MBaseController
    {
        /// <summary>
        /// 后台登录
        /// </summary>
        /// <returns></returns>
        public ActionResult login() {
            return View();
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public ActionResult logout() {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("login");
        }

        /// <summary>
        /// 后台主页
        /// </summary>
        /// <returns></returns>
        [SysAuthorize(RoleType = SysRolesType.后台)]
        public ActionResult index()
        {
            SysManagerUser mu = new SysManagerUser(User.Identity.Name);
            ViewBag.userMenu = mu.getUserMenu();
            ViewBag.userinfo = mu;
            return View();
        }

        /// <summary>
        /// 工作台
        /// </summary>
        /// <returns></returns>
        [SysAuthorize(RoleType = SysRolesType.后台)]
        public ActionResult workTab() {
            return View();
        }
    }

    /// <summary>
    /// 后台用户相关
    /// </summary>
    public class ManagerUserController : MBaseController {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [SysAuthorize(RoleType = SysRolesType.后台)]
        public ActionResult changepwd(string pageId = null)
        {
            setPageId(pageId);
            return View();
        }

        /// <summary>
        /// 当前登陆人的信息
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        [SysAuthorize(RoleType = SysRolesType.后台)]
        public ActionResult userInfo(string pageId = null) {
            setPageId(pageId);
            SysManagerUser user = new SysManagerUser(User.Identity.Name);
            ViewBag.user = user;
            return View();
        }

        /// <summary>
        /// 后台用户列表
        /// </summary>
        /// <param name="pageId">界面ID</param>
        /// <param name="viewset">界面设置</param>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        [SysAuthorize(RoleType = SysRolesType.后台)]
        public ActionResult userList(ViewModelSearchUserBaseRequest query, string pageId = null,string viewset = "default")
        {
            setPageId(pageId);
            ViewBag.query = query;
            return View("userList_" + viewset);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public ActionResult editUser(string loginName,string pageId = null) {
            setPageId(pageId);
            ViewBag.loginName = loginName;
            return View();
        }
    }

    /// <summary>
    /// 后台消息
    /// </summary>
    public class ManagerMsgController : MBaseController {

        /// <summary>
        /// 用户日志
        /// </summary>
        /// <param name="condtion"></param>
        /// <param name="pageId"></param>
        /// <param name="viewset"></param>
        /// <returns></returns>
        [SysAuthorize(RoleType = SysRolesType.后台)]
        public ActionResult userLog(ViewModelMsgSearchUserLogReqeust condtion, string pageId,string viewset = "default") {
            setPageId(pageId);
            ViewBag.condtion = condtion;
            ViewModelSearchUserBaseRequest searchUserRequest = new ViewModelSearchUserBaseRequest();
            ViewBag.searchUserRequest = searchUserRequest;
            return View("userLog_" + viewset);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="condtion"></param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        [SysAuthorize(RoleType = SysRolesType.后台)]
        public ActionResult exceptionLog(ViewModelMsgSearchUserLogReqeust condtion, string pageId)
        {
            setPageId(pageId);
            ViewBag.condtion = condtion;
            return View();
        }
    }
}