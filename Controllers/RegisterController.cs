using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Controllers
{
    public class RegisterController : Controller
    {
        /// <summary>
        /// Chuyển đổi một chuỗi về SHA256
        /// </summary>
        /// <param name="clearText">Bản rõ cần băm</param>
        /// <returns></returns>
        public static string GetSHA256(string clearText)
        {
            SHA256CryptoServiceProvider sHA256 = new SHA256CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(clearText);
            byte[] targetData = sHA256.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveData(User model)
        {
            model.EmailConfirm = false;
            model.UserRoleId = 2;
            model.MatKhau = GetSHA256(model.MatKhau);
            DataProvider.Entities.Users.Add(model);
            DataProvider.Entities.SaveChanges();
            BuildEmailTemplate(model.Id);
            return Json("Đăng ký thành công", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Confirm(int regId)
        {
            ViewBag.regID = regId;
            return View();
        }

   
        [HttpPost]
        public JsonResult RegisterConfirm(int regId)
        {
            User Data = DataProvider.Entities.Users.Where(x => x.Id == regId).FirstOrDefault();
            Data.EmailConfirm = true;
            DataProvider.Entities.SaveChanges();
            var msg = "Your Email Is Verified!";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = DataProvider.Entities.Users.Where(x => x.Id == regID).FirstOrDefault();
            var url = "http://localhost:58136/" + "Register/Confirm?regId=" + regID;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Tạo tài khoản thành công", body, regInfo.Email);
        }

        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "nhomhoctapnho123@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            client.Credentials = new System.Net.NetworkCredential("nhomhoctapnho123@gmail.com", "Hocvienkythuatmatma123");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}