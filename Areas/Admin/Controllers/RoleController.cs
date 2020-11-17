using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private static readonly ILog logger =
           LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Admin/Role
        public ActionResult ListRole()
        {
            List<UserRole> lstUserRole = DataProvider.Entities.UserRoles.ToList();
            return View(lstUserRole);
        }

        public JsonResult AddRole(int Id, string TenRole, string MoTa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserRole ObjRole = null;
                    bool isAdd = true;
                    if (Id > 0)
                    {
                        ObjRole = DataProvider.Entities.UserRoles.Find(Id);
                    }
                    if (ObjRole != null)
                    {
                        isAdd = false;
                    }
                    else
                    {
                        ObjRole = new UserRole();
                    }
                    //lấy thông tin từ form
                    ObjRole.Id = Id;
                    ObjRole.MoTa = MoTa;
                    ObjRole.TenRole = TenRole;

                    if (isAdd)
                    {
                        DataProvider.Entities.UserRoles.Add(ObjRole);
                    }
                    DataProvider.Entities.SaveChanges();
                    return Json(ObjRole.TenRole, JsonRequestBehavior.AllowGet);
                }
                //mặc định trả về rỗng
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Hàm lấy thông tin Role dạng Json
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetRole(string Id)
        {
            if (ModelState.IsValid)
            {
                #region Lấy dữ liệu về loại người dùng
                UserRole objReturn = null;
                int RoleId = -1;
                int.TryParse(Id, out RoleId);
                if (RoleId != -1)
                {
                    UserRole objTemp = DataProvider.Entities.UserRoles.Where(o => o.Id == RoleId).First();
                    objReturn = new UserRole();

                    //Lấy đối tượng cần trả về vì đối tượng EF sẽ có thông tin quan hệ giữa bảng khác
                    objReturn.Id = objTemp.Id;
                    objReturn.TenRole = objTemp.TenRole;
                    objReturn.MoTa = objTemp.MoTa;
                }
                #endregion
                JsonResult jsonRole = Json(objReturn,
                JsonRequestBehavior.AllowGet);
                return jsonRole;
            }
            //mặc định trả về rỗng
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Remove(string Id)
        {
            try
            {
                int RoleId = -1;
                int.TryParse(Id, out RoleId);
                UserRole objRole = DataProvider.Entities.UserRoles.Where(o => o.Id == RoleId).First();
                //if (objRole != null && objRole.Users.Count > 0)
                //{
                //    return Json("Role có người dùng tham chiếu. Xóa hết người dùng thuộc Role trước", JsonRequestBehavior.AllowGet);
                //}
                if (objRole != null)
                {
                    DataProvider.Entities.UserRoles.Remove(objRole);
                    DataProvider.Entities.SaveChanges();
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                //mặc định trả về rỗng
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
    }
}