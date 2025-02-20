using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ProjectServiceEntity
{
    [Key]
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;
    public int ServiceId { get; set; }
    public ServiceEntity Service { get; set; } = null!;
    public decimal Price { get; set; }
}