using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiRESTService.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            string line;
            List<List<String>> lines = new List<List<String>>();

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/NewFtpDir/"), fileName);
                    file.SaveAs(path);

                    // Read the file and display it line by line.
                    System.IO.StreamReader file1 =
                       new System.IO.StreamReader(path);

                    while ((line = file1.ReadLine()) != null)
                    {
                        List<String> subLines = new List<String>();

                        var temp = line.Split(',');

                        for (int i = 0; i < temp.Length; i++)
                            subLines.Add(temp[i]);

                        lines.Add(subLines);
                    }
                    file1.Close();
                }
                ViewBag.fileName = file.FileName;
            }
            ViewBag.lines = lines;

            return View();
        }


        // GET: Home
        public ActionResult ShowList()
        {

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/NewFtpDir/");

            DirectoryInfo directory = new DirectoryInfo(sPath);
            ViewBag.files = directory.GetFiles().ToList();
           // var files = directory.GetFiles().ToList();


            return View();
        }
    }
}