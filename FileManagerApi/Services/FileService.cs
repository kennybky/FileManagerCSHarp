using FileManagerApi.Data;
using FileManagerApi.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FileManagerApi.Services
{
    public class FileService
    {

        private IHostingEnvironment _hostingEnvironment;
        private readonly AppSettings _appSettings;

        public FileService(IHostingEnvironment hostingEnvironment, IOptions<AppSettings> appSettings)
        {
            _hostingEnvironment = hostingEnvironment;
            _appSettings = appSettings.Value;
        }

        public async Task<bool> UploadFiles(ICollection<IFormFile> files)
        {
            foreach (var file in files)
            {
                try
                {

                    string folderName = _appSettings.DefaultFolder;
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    if (file.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("Upload Failed: " + ex.Message);
                }
            }
            return true;
        }

        public async Task<FileDownloadResult> Download(string filename)
        {
            try
            {
                if (filename == null)
                    throw new AppException("filename not present");

                string folderName = _appSettings.DefaultFolder;
                string webRootPath = _hostingEnvironment.WebRootPath;

                var path = Path.Combine(
                               webRootPath, folderName, filename);

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return new FileDownloadResult(memory, GetContentType(path), Path.GetFileName(path));
            } catch(Exception ex)
            {
                throw new AppException("There was an error with the download. " + ex.Message);
            }
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
