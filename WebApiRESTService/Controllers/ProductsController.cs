using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiRESTService.Models;
using System.Web.Http.Cors;
using System.IO;


//  AngularJS file upload used the native XMLHttpRequest() to post multiple files to a Web API controller class. 

namespace WebApiRESTService.Controllers
{
    public class UploadController : ApiController
    {

        // The first parameter restricts the domains that can access
        // this service. In this case it is open to all.
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // POST: api/Upload
        [HttpPost()]
        //public string UploadFiles()
        public string Post()
        {
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/FtpDir/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));

                        // ?? string a = hpf.ToString();
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return iUploadedCnt + "/" + hfc.Count + " Files Uploaded Successfully";
            }
            else
            {
                return "Upload Failed";
            }
        }


        // The first parameter restricts the domains that can access
        // this service. In this case it is open to all.
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // GET: api/Uploads
        //public List<FileInfo> GetUploadFiles()
        public IEnumerable<FileInfo> Get()
        {
            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/FtpDir/");

            DirectoryInfo directory = new DirectoryInfo(sPath);
            var files = directory.GetFiles().ToList();

            return files;
        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // GET: api/Uploads
        public List<FileInfo> Get(int id)
        {
            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/FtpDir/");

            DirectoryInfo directory = new DirectoryInfo(sPath);
            var files = directory.GetFiles().ToList();

            return files;
        }


        // The first parameter restricts the domains that can access
        // this service. In this case it is open to all.
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        // Delete: api/Uploads
        //public string DeleteUploadFiles(int  filename )
        //{
        //    // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
        //    string sPath = "";
        //    sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/FtpDir/");

        //    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
        //    if (!File.Exists(sPath + filename))
        //    {
        //        // SAVE THE FILES IN THE FOLDER.
        //        File.Delete(sPath + filename);
        //        return "Delete " + filename + " Successfully";
        //    }
        //    return "No " + filename;
        //}
    }
}
