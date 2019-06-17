using System;
using System.Collections;
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
    public class AdminUserController : Controller
    {
        public IAdminUser AdminUserService { get; set; }

        public IRole RoleService { get; set; }

        public ICity CityService { get; set; }
        // GET: AdminUser
        public ActionResult List(AdminUserConditionModel adminUserCondition)
        {
            var adminUsers = AdminUserService.GetAll();
            if (adminUserCondition.StartDateTime != DateTime.MinValue)
            {
                adminUsers = adminUsers.Where(a => a.CreateDateTime > adminUserCondition.StartDateTime).ToArray();
            }

            if (adminUserCondition.EndDateTime != DateTime.MinValue)
            {
                adminUsers = adminUsers.Where(a => a.CreateDateTime < adminUserCondition.StartDateTime).ToArray();
            }

            if (!string.IsNullOrEmpty(adminUserCondition.Name))
            {
                adminUsers = adminUsers.Where(a => a.Name.ToLowerInvariant().Contains(adminUserCondition.Name.ToLowerInvariant())).ToArray();
            }

            return View(adminUsers);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var roles = RoleService.GetAll();
            var cities = CityService.GetAll().ToList();
            cities.Insert(0, new CityDTO { Id = 0, Name = "总部" });
            AdminUserAddNewGetViewModel addNewGetView = new AdminUserAddNewGetViewModel();
            addNewGetView.Roles = roles;
            addNewGetView.Cities = cities.ToArray();
            return View(addNewGetView);
        }

        [HttpPost]
        public ActionResult Add(AdminUserAddNewModel adminUser)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = Web.Common.WebCommonHelper.GetValidMsg(ModelState)
                });
            }
            // todo 检查用户名，手机号是否重复
            adminUser.CityId = adminUser.CityId > 0 ? adminUser.CityId : null;
            long adminUserId = AdminUserService.AddAdminUser(adminUser.Name, adminUser.PhoneNum, adminUser.Password, adminUser.Email,
                adminUser.CityId);
            RoleService.AddRoleIds(adminUserId, adminUser.RoleIds);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult Delete(long id)
        {
            if (AdminUserService.MarkDeleted(id))
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error" });
            }
        }

        public ActionResult BatchDelete(long[] ids)
        {
            foreach (long id in ids)
            {
                AdminUserService.MarkDeleted(id);
            }

            return Json(new AjaxResult { Status = "ok" });
        }


        [HttpGet]
        public ActionResult Edit(long id)
        {
            var adminUser = AdminUserService.GetById(id);
            AdminUserEditGetViewModel adminUserEdit = new AdminUserEditGetViewModel();
            adminUserEdit.AdminUserDto = adminUser;
            adminUserEdit.RoleDtos = RoleService.GetAll();
            var cities = CityService.GetAll().ToList();
            cities.Insert(0, new CityDTO() { Id = 0, Name = "总部" });
            adminUserEdit.CityDtos = cities.ToArray();
            return View(adminUserEdit);
        }

        [HttpPost]
        public ActionResult Edit(AdminUserEditModel AdminUser)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = Web.Common.WebCommonHelper.GetValidMsg(ModelState)
                });
            }
            if (string.IsNullOrEmpty(AdminUser.Password))
            {
                AdminUserService.UpdateAdminUser(AdminUser.Id, AdminUser.Name, AdminUser.PhoneNum, AdminUser.Email, AdminUser.CityId);
            }
            else
            {
                AdminUserService.UpdateAdminUser(AdminUser.Id, AdminUser.Name, AdminUser.PhoneNum, AdminUser.Password, AdminUser.Email, AdminUser.CityId);
            }

            RoleService.UpdateRoleIds(AdminUser.Id, AdminUser.RoleIds);

            return Json(new AjaxResult() { Status = "ok" });
        }

    }
}