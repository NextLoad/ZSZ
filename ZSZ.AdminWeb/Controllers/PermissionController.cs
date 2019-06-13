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
    public class PermissionController : Controller
    {
        public IPermission PermService { get; set; }
        // GET: Permission
        public ActionResult List()
        {
            PermissionDTO[] perms = PermService.GetAll();
            return View(perms);
        }

        public ActionResult Delete(long id)
        {
            PermService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult BatchDelete(long[] ids)
        {
            foreach (long id in ids)
            {
                PermService.MarkDeleted(id);
            }

            return Json(new AjaxResult() { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(PermissionAddModel permissionAdd)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = Web.Common.WebCommonHelper.GetValidMsg(ModelState)
                });
            }

            PermService.AddNewPermission(permissionAdd.Name, permissionAdd.Description);
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            PermissionDTO permissionDto = PermService.GetById(id);
            return View(permissionDto);
        }

        [HttpPost]
        public ActionResult Edit(PermissionEditModel permissionEdit)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = Web.Common.WebCommonHelper.GetValidMsg(ModelState)
                });
            }
            PermService.UpdatePermission(permissionEdit.Id, permissionEdit.Name, permissionEdit.Description);
            return Json(new AjaxResult() { Status = "ok" });
        }

        public ActionResult Search(string name)
        {
            PermissionDTO permissionDto = PermService.GetByName(name);
            PermissionDTO[] permissionDtos = new PermissionDTO[]{};
            return View(permissionDtos);
        }

    }
}