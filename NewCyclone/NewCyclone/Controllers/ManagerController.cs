using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using NewCyclone.Models;

namespace NewCyclone.Controllers
{
    /// <summary>
    /// 后台系统界面控制
    /// </summary>
    public class ManagerController : Controller
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
        [Authorize(Roles = "admin,user")]
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
        [Authorize(Roles = "admin,user")]
        public ActionResult workTab() {
            return View();
        }
    }
}