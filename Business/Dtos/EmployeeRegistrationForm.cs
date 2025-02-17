namespace Business.Dtos;

public class EmployeeRegistrationForm
{
    public int Id { get; set; }
    public string EmploymentNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
