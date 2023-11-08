using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using SystemManagement.Shared.Utilities;

namespace SystemManagement.Client.Controllers
{
    public class UploaderController : BaseController
    {
        [HttpPost]
        public void DeleteFiles(params string[] fileNames)
        {
            var uploadedFilesPath = Settings.UploaderFilesLocations;
            var uploadPath = Server.MapPath(uploadedFilesPath);

            Uploader.DeleteFiles(uploadPath, fileNames);
        }

        [HttpPost]
        public ActionResult Upload()
        {
            var uploadedFilesPath = Settings.UploaderFilesLocations;
            var isFileUploadFinished = false;
            var fileNames = new List<string>();

            foreach (string file in Request.Files)
            {
                var FileDataContent = Request.Files[file];
                if (FileDataContent != null && FileDataContent.ContentLength > 0)
                {
                    var stream = FileDataContent.InputStream;
                    var fileName = Path.GetFileName(FileDataContent.FileName);
                    var UploadPath = Server.MapPath(uploadedFilesPath);
                    _ = Directory.CreateDirectory(UploadPath);
                    var path = Path.Combine(UploadPath, fileName);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    using (var fileStream = System.IO.File.Create(path))
                    {
                        stream.CopyTo(fileStream);
                    }

                    isFileUploadFinished = Uploader.MergeFile(path);

                    fileNames.Add(fileName);
                }
            }
            return Json(new
            {
                fileNames,
                isFileUploadFinished
            });
        }
    }
}