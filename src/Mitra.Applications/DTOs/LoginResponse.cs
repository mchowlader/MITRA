namespace Mitra.Applications.DTOs;

public record LoginResponse(bool Success, string Message = null!, string Token = null!)
{
}