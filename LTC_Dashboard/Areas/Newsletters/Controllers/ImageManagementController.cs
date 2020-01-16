using System;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using LTCDataModel.NewsLetter;

namespace LTC_Dashboard.Areas.Newsletters.Controllers
{
    [Area("Newsletters")]
    public class ImageManagementController : BaseController
    {

        private readonly IHostingEnvironment _webHostEnvironment;
        private const string UserImagesPath = "/Content/ImageManagement/{0}/{1}";
        private const string OfficeImagesPath = "/Content/ImageManagement/{0}";
        // GET: ImageManagement
        public ActionResult Index()
        {
            @ViewBag.OfficeName = OfficeName;

            return View();
        }
        public ImageManagementController(IHostingEnvironment webHostEnvironment):base(webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<JsonResult> FileUpload()
        {
            try
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string contentRootPath = _webHostEnvironment.ContentRootPath;

                var file = Request.Form.Files[0];
                if (file != null)
                {
                    string userId = CurrentLoggedInUserId.ToString();
                    string officeId = CurrentOfficeId.ToString();


                    string path = string.Format(UserImagesPath, officeId, userId);

                    //var blob = System.IO.Path.Combine(webRootPath, path);
                    var blob = webRootPath + path;
                    bool exists = System.IO.Directory.Exists(blob);

                    if (!exists)
                        System.IO.Directory.CreateDirectory(blob);


                    string pic = System.IO.Path.GetFileName(file.FileName);
                    path = System.IO.Path.Combine(blob, pic);
                   
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }

                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false });
            }
        }



        [HttpPost]
        public async Task<JsonResult> FileUpload2(IFormFile file)
        {
            if (file != null)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string userId = CurrentLoggedInUserId.ToString();
                string officeId = CurrentOfficeId.ToString();


                string path = string.Format(UserImagesPath, officeId, userId);
                //var blob = System.IO.Path.Combine(webRootPath, path);
                var blob = webRootPath + path;
                bool exists = System.IO.Directory.Exists(blob);

                if (!exists)
                    System.IO.Directory.CreateDirectory(blob);


                string pic = System.IO.Path.GetFileName(file.FileName);
                path = System.IO.Path.Combine(blob, pic);
                // file is uploaded
                //file.SaveAs(blob);
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }


            return Json(true);
        }
        [HttpPost]
        public JsonResult DeleteImage([FromBody]DeleteModel model)
        {
            try
            {

                string webRootPath = _webHostEnvironment.WebRootPath;
                string contentRootPath = _webHostEnvironment.ContentRootPath;

                if (model.file != null)
                {
                    string userId = CurrentLoggedInUserId.ToString();
                    string officeId = CurrentOfficeId.ToString();


                    string path = string.Format(UserImagesPath, officeId, userId);

                    //var blob = System.IO.Path.Combine(webRootPath, path);
                    var blob = webRootPath + path;
                    bool exists = System.IO.Directory.Exists(blob);

                    if (!exists)
                        System.IO.Directory.CreateDirectory(blob);

                    bool fileExists = System.IO.File.Exists(blob +"/"+ model.file);
                    if (fileExists)
                        System.IO.File.Delete(blob +"/"+ model.file);

                }

                return Json(new { Success = true });

                 
            }
            catch (Exception ex)
            {
                var obj = JsonConvert.SerializeObject(ex.Message);
                return Json(obj);
            }
        }
        [HttpGet]
        public JsonResult GetUserImages()
        {
            try
            {

            string webRootPath = _webHostEnvironment.WebRootPath;
            string userId = CurrentLoggedInUserId.ToString();
            string officeId = CurrentOfficeId.ToString();


            string path = string.Format(UserImagesPath, officeId, userId);
            //var blob = System.IO.Path.Combine(webRootPath, path);
                var blob = webRootPath + path;
                bool exists = System.IO.Directory.Exists(blob);

            if (!exists)
                System.IO.Directory.CreateDirectory(blob);


            var arr = new ArrayList();
            DirectoryInfo apple = new DirectoryInfo(blob);
            foreach (var file in apple.GetFiles("*"))
            {

                arr.Add(new { text = file.Name, value = HttpUtility.UrlEncode(Request.Scheme + "://" + Request.Host.Host + path + "/" + file.Name) });
            }

            var obj = JsonConvert.SerializeObject(arr);
            return Json(obj);
            }
            catch (Exception ex)
            {
                var obj = JsonConvert.SerializeObject(ex.Message);
                return Json(obj);
            }
        }

        [HttpGet]
        public JsonResult GetUserImagesLink()
        {
            try
            {

                string webRootPath = _webHostEnvironment.WebRootPath;
                string userId = CurrentLoggedInUserId.ToString();
                string officeId = CurrentOfficeId.ToString();


                string path = string.Format(UserImagesPath, officeId, userId);
                //var blob = System.IO.Path.Combine(webRootPath, path);
                var blob = webRootPath + path;
                bool exists = System.IO.Directory.Exists(blob);

                if (!exists)
                    System.IO.Directory.CreateDirectory(blob);


                var arr = new ArrayList();
                DirectoryInfo dInfo = new DirectoryInfo(blob);
                foreach (var file in dInfo.GetFiles("*"))
                {
                        arr.Add(new { name = file.Name, path = path + "/" + file.Name });
                }


                var obj = JsonConvert.SerializeObject(arr);
                return Json(obj);
            }
            catch (Exception ex)
            {
                var obj = JsonConvert.SerializeObject(ex.Message);
                return Json(obj);
            }
        }

        [HttpGet]
        public JsonResult GetOfficeImages()
        {
            try
            {

            string userId = CurrentLoggedInUserId.ToString();
            string officeId = CurrentOfficeId.ToString();

            string webRootPath = _webHostEnvironment.WebRootPath;

            string path = string.Format(OfficeImagesPath, officeId);

                var blob = webRootPath + path;
                //var blob = System.IO.Path.Combine(webRootPath, path);
                bool exists = System.IO.Directory.Exists(blob);

            if (!exists)
                System.IO.Directory.CreateDirectory(blob);


            var arr = new ArrayList();
            DirectoryInfo dInfo = new DirectoryInfo(blob);
            foreach (var userDir in dInfo.GetDirectories("*"))
            {
                foreach (var file in userDir.GetFiles("*"))
                {
                    arr.Add(new { name = file.Name, path = path + "/" + userDir.Name + "/" + file.Name });
                }
            }

            var obj = JsonConvert.SerializeObject(arr);
            return Json(obj);
            }
            catch (Exception ex)
            {
                var obj = JsonConvert.SerializeObject(ex);
                return Json(obj);
            }

        }
    }
}