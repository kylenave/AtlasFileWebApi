using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtlasFileWebApi.Controllers
{
    [Route("api/[controller]")]
    public class DocFileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // POST api/values
    [HttpPost]
    [AllowAnonymous]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            //Do something with the files here. 
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = @"temp.txt";

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    filePath = @"Uploads/" + formFile.FileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath});
        }
    }
}
