using System;
namespace BibliotecaUdeA.Business.Dtos
{
    public class BaseResponse<T>
    {
        public Exception Exception { get; set; }
        public T Data { get; set; }
    }
}
