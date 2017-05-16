using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.IO;

namespace FineuploaderMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            return View();
        }

		public ActionResult ProcessUpload(HttpPostedFileWrapper qqfile)
		{

			string extension = qqfile.ContentType.Split('/')[1];
			string filetype = qqfile.ContentType.Split('/')[0];
			Int32 filesize = qqfile.ContentLength / 1000;

			if (qqfile != null && filesize > 0 && (filetype == "image" || (extension == "pdf")))
			{
				{
					var fileName = Path.GetFileName(qqfile.FileName);
					Random rnd = new Random();
					string original_name = qqfile.FileName;
					string sTime = System.DateTime.Now.ToString("yyyyMMdd-HH-mm-ss-FFF");
					string NewFileName = sTime + "Sh7ne" + rnd.Next(0, 9999) + '.' + extension;
					var path = Path.Combine(Server.MapPath("~/Content/Uploads"), NewFileName);
					try
					{
						if (System.IO.File.Exists(path))
							System.IO.File.Delete(path);
						qqfile.SaveAs(path);
					}
					catch (Exception ex)
					{
						return Json(new { success = false, error = ex.Message });
					}
				}
				return Json(new { success = true });

			}
			else
			{
				return Json(new { success = false, message = qqfile.FileName + "has an invalid extension. Valid extension(s): jpeg, jpg, pdf, png, or other images format" });
			}

		}
    }
}
