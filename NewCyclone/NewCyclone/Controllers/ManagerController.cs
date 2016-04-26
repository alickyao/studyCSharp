using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        /// 后台主页
        /// </summary>
        /// <returns></returns>
        // GET: Manager
        public ActionResult index()
        {
            SysManagerUser mu = new SysManagerUser();
            mu.role = "admin";
            ViewBag.userMenu = mu.getUserMenu();
            return View();
        }

        /// <summary>
        /// 工作台
        /// </summary>
        /// <returns></returns>
        public ActionResult workTab() {
            return View();
        }
    }
}