using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagerApi.Data
{
    public class FileDownloadResult
    {
        internal MemoryStream Stream { get; private set; }
        internal string ContentType { get; private set; }
        internal string Path { get; private set; }

        public FileDownloadResult(MemoryStream memstm, string content_type, string path)
        {
            Stream = memstm;
            ContentType = content_type;
            Path = path;
        }



        
    }
}
