using System.Net;

namespace Application.Common.DTO
{
    public class ResponseDTO<T>
    {
        public ErrorDTO Error { get; set; }
        public HttpStatusCode Status { get; set; }
        public T Data { get; set; }
    }
}