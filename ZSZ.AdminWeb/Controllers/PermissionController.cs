using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.App_Start;
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

        [CheckPermission("permission.list")]
        public ActionResult List()
        {
            PermissionDTO[] perms = PermService.GetAll();
            return View(perms);
        }

        [CheckPermission("permission.delete")]
        public ActionResult Delete(long id)
        {
            PermService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }

        [CheckPermission("permission.delete")]
        public ActionResult BatchDelete(long[] ids)
        {
            foreach (long id in ids)
            {
                PermService.MarkDeleted(id);
            }

            return Json(new AjaxResult() { Status = "ok" });
        }

        [CheckPermission("permission.add")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [CheckPermission("permission.add")]
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

        [CheckPermission("permission.edit")]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PermissionDTO permissionDto = PermService.GetById(id);
            return View(permissionDto);
        }

        [CheckPermission("permission.edit")]
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

        [CheckPermission("permission.list")]
        public ActionResult Search(string name)
        {
            PermissionDTO permissionDto = PermService.GetByName(name);
            IList<PermissionDTO> permissionDtos = new List<PermissionDTO>();
            if (permissionDto != null)
            {
                permissionDtos.Add(permissionDto);
            }
            return View("List",permissionDtos.ToArray());
        }

    }
}