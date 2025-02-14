using Data.Entities;

namespace Business.Models;

public class Project
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public int StatusId { get; set; }
    public string StatusType { get; set; } = null!;
    public int EmployeeId { get; set; }
    public string EmployeeFirstName { get; set; } = null!;
    public string EmployeeLastName { get; set; } = null!;
    public string ProjectManager { get; set; } = null!;
    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}


