using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace BeauitySaloonWeb.CustomsValidations
{
    public static class UploadPicture
    {
        public static string WriteFile(HttpPostedFileWrapper file, string FolderName)
        {
            bool WriteFileWithRoot = true;
            string fileName = null;
            string pathwithfilename;

            if (WriteFileWithRoot == true)
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var pathBuilt = Path.Combine(HostingEnvironment.MapPath($"~/Uploads/{FolderName}/"));
                fileName = Path.GetFileName(file.FileName);
                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }
                var path = Path.Combine(HostingEnvironment.MapPath($"~/Uploads/{FolderName}/"), fileName);
                file.SaveAs(path);
            }
            pathwithfilename = $"Uploads/{FolderName}/" + fileName;
            return pathwithfilename;
        }
    }
}