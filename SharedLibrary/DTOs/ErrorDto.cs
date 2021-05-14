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

        }

        public ErrorDto(string error, bool isShow)
        {
            Errors = new List<string>();

            Errors.Add(error);
            _isShow = isShow;
        }

        public ErrorDto(List<string> error, bool isShow)
        {
            Errors = new List<string>();

            Errors = error;
            _isShow = isShow;
        }



    }
}
