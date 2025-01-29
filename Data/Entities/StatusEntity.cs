using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class StatusEntity
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(20)")]
    public string StatusType { get; set; } = null!;
}