using Microsoft.AspNetCore.Http;
using Mitra.Applications.Helper;

namespace Mitra.Applications.Response;
public class PayloadResponse<TEntity> where TEntity : class
{
    public PayloadResponse()
    {
        _httpContextAccessor = new HttpContextAccessor();
        this.RequestURL = _httpContextAccessor.HttpContext != null ? $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}{_httpContextAccessor.HttpContext.Request.Path}" : "";
        this.ResponseTime = Utilities.GetRequestResponseTime();
    }
    private readonly IHttpContextAccessor _httpContextAccessor;
    public bool Success { get; set; }
    public string RequestTime { get; set; }
    public string ResponseTime { get; set; }
    public string RequestURL { get; set; }
    public List<string> Message { get; set; }
    public TEntity Payload { get; set; }
    public string PayloadType { get; set; }
}