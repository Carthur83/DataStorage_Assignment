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

    public string CurrentStatus { get; set; } = null!;

    [Required]
    public decimal TotalPrice { get; set; }

    [Required]
    public CustomerRegistrationForm Customer { get; set; } = new();

    [Required]
    public EmployeeRegistrationForm ProjectManager { get; set; } = new();

    [Required]
    public ServiceRegistrationForm Service { get; set; } = new();
}
