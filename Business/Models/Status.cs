using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class Status
{
    public int Id { get; set; }
    public string StatusType { get; set; } = null!;
}
