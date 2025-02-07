namespace Business.Dtos;

public class ServiceRegistrationForm
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = null!;
    public decimal Price { get; set; }
}
