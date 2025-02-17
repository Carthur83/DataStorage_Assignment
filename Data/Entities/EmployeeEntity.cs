using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class EmployeeEntity
{
    [Key]
    public int Id { get; set; }

    public string EmploymentNumber { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;
   
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;
}
