using System.ComponentModel.DataAnnotations;

namespace TaskList.Application.Commands;

public class CreateTaskListCommand
{
    [StringLength(255, MinimumLength = 1)]
    public string Name { get; set; }
    [Required]
    public string OwnerId { get; set; }
}