using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }


    [Column(TypeName = "nvarchar(50)")]
    public string ProjectName { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }


    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    public decimal TotalPrice { get; set; }


    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;


    public int StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;

    public string EmployeeId { get; set; } = null!;
    public EmployeeEntity Employee { get; set; } = null!;


    public int ServiceId { get; set; }
    public ServiceEntity Service { get; set; } = null!;
}
