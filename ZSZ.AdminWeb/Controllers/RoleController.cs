using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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
            if (!ModelState.IsValid)
            {
                string msg = Web.Common.WebCommonHelper.GetValidMsg(ModelState);
                return Json(new AjaxResult() { Status = "error", Msg = msg });
            }
            //todo 检查角色名是否重复
            long roleId = roleService.AddNewRole(roleAddModel.Name);
            permService.AddPermIds(roleId, roleAddModel.PermIds);
            return Json(new AjaxResult() { Status = "ok" });
        }

        [HttpGet]
        public ActionResult EditRole(long id)
        {
            RoleDTO roleDto = roleService.GetById(id);
            PermissionDTO[] permissionDtos = permService.GetByRoleId(id);
            PermissionDTO[] permissionDtoAll = permService.GetAll();
            RoleEditGetModel roleEditGet = new RoleEditGetModel();
            roleEditGet.PermissionDtoAll = permissionDtoAll;
            roleEditGet.PermissionDtoHas = permissionDtos;
            roleEditGet.RoleDto = roleDto;
            return View(roleEditGet);
        }

        [HttpPost]
        public ActionResult EditRole(RoleEditModel roleEdit)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                roleService.UpdateRole(roleEdit.Id, roleEdit.Name);
                permService.UpdatePermIds(roleEdit.Id, roleEdit.PermIds);
                transaction.Complete();
            }

            return Json(new AjaxResult() { Status = "ok" });
        }


        public ActionResult DeleteRole(long id)
        {
            roleService.MarkDeleted(id);
            return Json(new AjaxResult() { Status = "ok" });
        }

        public ActionResult DeleteBatchRole(long[] ids)
        {
            foreach (long id in ids)
            {
                roleService.MarkDeleted(id);
            }
            return Json(new AjaxResult() { Status = "ok" });
        }

    }
}