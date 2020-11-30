﻿using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class DataController : Controller
    {      
        /// <summary>
        /// Show a list of Function
        /// </summary>
        /// <returns></returns>
        public ActionResult ListFunc()
        {
            return View(DataProvider.Entities.Function.ToList());
        }

        [HttpGet]
        public ActionResult ImpFunc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImpFunc(HttpPostedFileBase FileUpload)
        {
            List<string> data = new List<string>();
            if (FileUpload != null && FileUpload.ContentLength > 0)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel" 
                    || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    //tạo đường dẫn
                    string uploadPath = ConfigurationManager.AppSettings["fileUpload"];
                    //nếu đường dẫn ko có thì tạo
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    FileUpload.SaveAs(uploadPath + filename);
                    string pathToExcelFile = uploadPath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.
                            Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; " +
                            "Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.
                            Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};" +
                            "Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\";", pathToExcelFile);
                    }
                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);                  
                    var artistAlbums = from a in excelFile.Worksheet<Function>(sheetName) 
                                       select a;

                    List<Function> list =  artistAlbums.ToList();                  
                    //Xóa các hàng cũ
                    var rows = from o in DataProvider.Entities.Function
                               select o;
                    foreach (var row in rows)
                    {
                        DataProvider.Entities.Function.Remove(row);
                    }
                    DataProvider.Entities.SaveChanges();
                    foreach (var a in artistAlbums)
                    {
                        try
                        {                            
                                if (                                   
                                    !string.IsNullOrEmpty(a.TenChucNang) 
                                    && !string.IsNullOrEmpty(a.MoTa)  
                                    && !string.IsNullOrEmpty(a.TenForm)
                                    )
                                {
                                    Function F = new Function();                                  
                                    F.TenChucNang = a.TenChucNang;
                                    F.MoTa = a.MoTa;
                                    F.TenForm = a.TenForm;
                                    DataProvider.Entities.Function.Add(F);
                                    DataProvider.Entities.SaveChanges();
                                        a.Id++;
                                }
                                else
                                {
                                    data.Add("<ul>");
                                    if (a.TenChucNang == "" || a.TenChucNang == null) 
                                        data.Add("<li> Yêu cầu nhập tên chức năng</li>");
                                    if (a.MoTa == "" || a.MoTa == null) 
                                        data.Add("<li> Yêu cầu nhập mô tả</li>");
                                    if (a.TenForm == "" || a.TenForm == null) 
                                        data.Add("<li> Yêu cầu nhập tên form</li>");

                                    data.Add("</ul>");
                                    data.ToArray();
                                    return Json(data, JsonRequestBehavior.AllowGet);
                                }
                            
                        }

                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }

                            }
                        }
                    }
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Chỉ được tải file Excel</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Yêu cầu chọn file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }           
        }


        [HttpGet]
        public ActionResult ImpRoleAndFunc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImpRoleAndFunc(HttpPostedFileBase FileUpload)
        {
            List<UserRole> ListRole = DataProvider.Entities.UserRoles.ToList();
            List<Function> ListFunction = DataProvider.Entities.Function.ToList();
            if (ListFunction.Count == 0)
            {
                TempData["FunctionEmpty"] = ("Vui lòng import dữ liệu về chức năng");
                return RedirectToAction("ImpFunc", "Data", "Admin");
            }    
               
            List<string> data = new List<string>();
            if (FileUpload != null && FileUpload.ContentLength > 0)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel"
                    || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    //tạo đường dẫn
                    string uploadPath = ConfigurationManager.AppSettings["fileUpload"];
                    //nếu đường dẫn ko có thì tạo
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    FileUpload.SaveAs(uploadPath + filename);
                    //đọc file Excel
                    string pathToExcelFile = uploadPath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.
                            Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; " +
                            "Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.
                            Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};" +
                            "Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\";", pathToExcelFile);
                    }
                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet(sheetName)
                                       select a;

                    List<Row> list = artistAlbums.ToList();
                    //Xóa các cột cũ trong DB
                    var rows = from o in DataProvider.Entities.UserRoleAndFunctions
                               select o;
                    foreach (var row in rows)
                    {
                        DataProvider.Entities.UserRoleAndFunctions.Remove(row);
                    }
                    DataProvider.Entities.SaveChanges();

                    //Add các cột mới vào DB
                    foreach (var a in artistAlbums)
                    {
                        try
                        {

                            if (true)
                            {
                                UserRoleAndFunction RF = new UserRoleAndFunction();
                                Function F = new Function();
                                UserRole R = new UserRole();                               
                                F = ListFunction.FirstOrDefault(o => o.TenChucNang == a[0].Value.ToString());
                                R = ListRole.FirstOrDefault(o => o.TenRole == a[1].Value.ToString());
                                RF.FuctionId = F.Id;
                                RF.UserRoleId = R.Id;
                                DataProvider.Entities.UserRoleAndFunctions.Add(RF);
                                DataProvider.Entities.SaveChanges();                               
                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a[0].Value.ToString() == null)
                                    data.Add("<li> Yêu cầu nhập tên chức năng</li>");
                                if (a[1].Value.ToString() == null)
                                    data.Add("<li> Yêu cầu nhập tên quyền</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }

                            }
                        }
                    }
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Chỉ được tải file Excel</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Yêu cầu chọn file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}