using Microsoft.AspNetCore.Http;

namespace Mitra.Applications.Helper;
public class HttpContextAccessor : IHttpContextAccessor
{
    public HttpContextAccessor() { }

    public HttpContext HttpContext { get; set; }
}