using System.ComponentModel.DataAnnotations;

namespace webpract4.DTOs;

public class TeamDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;
}
