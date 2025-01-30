using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProjectRegistrationForm
{
    [Required]
    public string ProjectName { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string CustomerName { get; set; } = null!;

    public string CurrentStatus { get; set; } = null!;

    [Required]
    public ProjectManagerEntity ProjectManager { get; set; } = null!;

    [Required]
    public string ServiceName { get; set; } = null!;
}
