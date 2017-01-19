using Microsoft.AspNetCore.Mvc;
using Patronage17.Engine.Helpers;
using Patronage17.Web.Models.HomeViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Patronage17.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _appLocation;

        public HomeController()
        {
            this._appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Files()
        {
            var filePathes = FilesIoHelper.Instance.GetFiles(this._appLocation).ToList();
            var filesViewModel = new FilesViewModel()
            {
                Directory = this._appLocation
            };

            filePathes.ForEach(filePath =>
            {
                var metadata = FilesIoHelper.Instance.GetFileMetadata(filePath);
                filesViewModel.Files.Add(filePath, metadata);
            });
            
            return View(filesViewModel);
        }

        [HttpGet(("[controller]/filemetadata/{fileName}"))]
        public IActionResult FileMetadata(string fileName)
        {
            if(!string.IsNullOrEmpty(fileName))
            {
                var filePath = Path.Combine(this._appLocation, fileName);
                var fileMetadata = FilesIoHelper.Instance.GetFileMetadata(filePath);
                if(fileMetadata != null)
                {
                    return View(fileMetadata);
                }
            }
            return Content("Error");
        }
    }
}
