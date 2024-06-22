namespace Mitra.Applications.Response;

public class ServiceResponse<TEntity> where TEntity : class
{
    public TEntity Data { get; set; }
    public List<string>? Message { get; set; }
    public bool IsSuccess { get; set; }

    public static ServiceResponse<TEntity> RetriveSuccessfully(TEntity data, string message = null!)
    {
        return new ServiceResponse<TEntity>
        {
            Data = data,
            Message = new List<string> { message ?? "Data retrive successfully." },
            IsSuccess = true
        };
    }
    public static ServiceResponse<TEntity> UploadSuccessfully(TEntity data, string message = null!)
    {
        return new ServiceResponse<TEntity>
        {
            Data = data,
            Message = new List<string> { message ?? "Data upload Successfully."},
            IsSuccess = true
        };
    }
    public static ServiceResponse<TEntity> AddSuccessfully(TEntity data, string message = null!)
    {
        return new ServiceResponse<TEntity>
        {
            Data = data,
            Message = new List<string> { message ?? "Data added successfully." },
            IsSuccess = true
        };
    }
    public static ServiceResponse<TEntity> UpdateSuccessfully(TEntity data, string message = null!)
    {
        return new ServiceResponse<TEntity>
        {
            Data = data,
            Message = new List<string> { message ?? "Data update successfully." },
            IsSuccess = true
        };
    }
    public static ServiceResponse<TEntity> DeleteSuccessfully(TEntity data, string message = null!)
    {
        return new ServiceResponse<TEntity>
        {
            Data = data,
            Message = new List<string> { message ?? "Data delete successfully." },
            IsSuccess = true
        };
    }
    public static ServiceResponse<TEntity> Success(TEntity data, string message = null!)
    {
        return new ServiceResponse<TEntity>
        {
            Data = data,
            Message = new List<string> { message ?? "Request successfully." },
            IsSuccess = true
        };
    }
    public static ServiceResponse<TEntity> NotFound(string message = null!)
    {
        return new ServiceResponse<TEntity>
        {
            Data = null,
            Message = new List<string> { message ?? "No record found." },
            IsSuccess = false
        };
    }
    public static ServiceResponse<TEntity> Error(string message = null!)
    {
        if (string.IsNullOrEmpty(message))
        {
            if(message.Contains("Return the  transaction"))
            {
                message = "Something went worn. Pleae try again.";
            }
        }
        return new ServiceResponse<TEntity>
        {
            Data = null,
            Message = new List<string> { message ?? "There was a problem handling the request." },
            IsSuccess = false
        };
    }
}