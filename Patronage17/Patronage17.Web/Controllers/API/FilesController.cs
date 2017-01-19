using Microsoft.AspNetCore.Mvc;
using Patronage17.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Patronage17.Web.Controllers.API
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var files = FilesIoHelper.Instance.GetFiles(appLocation);

            return Json(files);
        }

        [HttpGet(("{fileName}"))]
        public IActionResult Index(string fileName)
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var filePath = Path.Combine(appLocation, fileName);
            var fileInfo = FilesIoHelper.Instance.GetFileMetadata(filePath);

            if (fileInfo != null)
            {
                return Json(fileInfo);
            }

            return Content(null);
        }


    }
}
