namespace Mitra.Domain.Model;
public class BaseModel
{
    public int Id { get; set; }
    public int CreateBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsDelete { get; set; }
    public bool IsActive { get; set; }
}