using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.Models;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.AdminWeb.Controllers
{
    public class RoleController : Controller
    {
        public IRole roleService { get; set; }

        public IPermission permService { get; set; }
        // GET: Role
        public ActionResult List()
        {
            RoleDTO[] roleDtos = roleService.GetAll();
            return View(roleDtos);
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            var permissionDtos = permService.GetAll();
            return View(permissionDtos);
        }

        [HttpPost]
        public ActionResult AddRole(RoleAddModel roleAddModel)
        {
            long roleId = roleService.AddNewRole(roleAddModel.Name);
            permService.AddPermIds(roleId, roleAddModel.PermIds);
            return Json(new AjaxResult() {Status = "ok"});
        }
    }
}