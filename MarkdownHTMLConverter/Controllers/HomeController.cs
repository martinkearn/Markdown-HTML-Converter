using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using System.IO;
using CommonMark;
using MarkdownHTMLConverter.ViewModels;

namespace MarkdownHTMLConverter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Viewer(IFormFile file)
        {
            if (file == null)
            {
                return RedirectToAction("Index");
            }

            //read incoming file to a string
            var html = string.Empty;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var fileContent = reader.ReadToEnd();

                //get full html string from file
                using (var writer = new StringWriter())
                {
                    CommonMarkConverter.ProcessStage3(CommonMarkConverter.Parse(fileContent), writer);
                    html += writer.ToString();
                }
            }

            var viewModel = new ViewerViewModel()
            {
                Html = html
            };

            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
