using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{
    public class ResponseDto<T> where T : class
    {
        public T Data { get; private set; }
        public int StatuseCode { get; private set; }
        public bool IsSuccess { get; private set; }
        public ErrorDto Error { get; private set; }


        public static ResponseDto<T> Success(T data, int statuseCode)
        {
            return new ResponseDto<T>
            {
                Data = data,
                StatuseCode = statuseCode,
                IsSuccess = true
            };
        }

        public static ResponseDto<T> Success(int statuseCode)
        {
            return new ResponseDto<T>
            {
                Data = default,
                StatuseCode = statuseCode,
                IsSuccess = true

            };
        }

        public static ResponseDto<T> Fail(ErrorDto errorDto, int statuseCode)
        {

            return new ResponseDto<T>
            {
                Error = errorDto,
                StatuseCode = statuseCode,
                IsSuccess = false

            };

        }

        public static ResponseDto<T> Fail(string errorMessage, int statuseCode, bool isShow)
        {
            ErrorDto errorDto = new ErrorDto(errorMessage, isShow);

            return new ResponseDto<T>
            {
                Error = errorDto,
                StatuseCode = statuseCode,
                IsSuccess = false

            };
        }
    }
}
