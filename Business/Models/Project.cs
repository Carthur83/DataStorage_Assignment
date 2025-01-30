namespace Business.Models;

public class Project
{
    public int Id { get; set; }

    public string ProjectName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string CustomerName { get; set; } = null!;
    public string CurrentStatus { get; set; } = null!;
    public string ProjectManager { get; set; } = null!;
    public string ServiceName { get; set; } = null!;
}
