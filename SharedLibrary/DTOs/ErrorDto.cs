using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{
    public class ErrorDto
    {
        public List<string> Errors { get; private set; }
        public bool _isShow { get; private set; }

        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public ErrorDto(string error, bool isShow) : base()
        {
            Errors.Add(error);
            _isShow = isShow;
        }

        public ErrorDto(List<string> error, bool isShow) : base()
        {
            Errors = error;
            _isShow = isShow;
        }



    }
}
