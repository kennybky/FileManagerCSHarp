using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagerApi.Data;
using FileManagerApi.Helpers;
using FileManagerApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private FileService _FileService;

        public FilesController(FileService fileService)
        {
            _FileService = fileService;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new AppException("Here an error");
        }

        [HttpGet("test")]
        public ActionResult Test()
        {
            return BadRequest("here");
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // validation here

            var download =  await _FileService.Download("KH.png");

            return File(download.Stream, download.ContentType, download.Path);
        }

        // POST api/<controller>
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] ICollection<IFormFile> files)
        {
            var succeed = await _FileService.UploadFiles(files);
            if (succeed)
            {
                return Ok();
            } else
            {
                return BadRequest();
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
