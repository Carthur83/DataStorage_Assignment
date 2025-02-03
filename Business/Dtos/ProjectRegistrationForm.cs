using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProjectRegistrationForm
{
    [Required]
    public string ProjectName { get; set; } = null!;

    [Required]
    public string StartDate { get; set; } = null!;

    [Required]
    public string EndDate { get; set; } = null!;

    [Required]
    public string CustomerName { get; set; } = null!;

    public string CurrentStatus { get; set; } = null!;

    [Required]
    public EmployeeEntity ProjectManager { get; set; } = new();

    [Required]
    public string ServiceName { get; set; } = null!;
}
