namespace Mitra.Domain.Entities;

public class BaseEntity
{
    public required int Id { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
}