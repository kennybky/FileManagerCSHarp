using Newtonsoft.Json;
using System;

namespace FileManagerApi.Models
{

    [Serializable]
    public class ErrorDetails
    {
        public ErrorDetails(int _statuscode, string _message)
        {
            StatusCode = _statuscode;
            Message = _message;
        }

        public ErrorDetails() { }

        public int StatusCode { get; set; }
        public string Message { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}