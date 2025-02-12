using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class CustomerRegistrationForm
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
}
