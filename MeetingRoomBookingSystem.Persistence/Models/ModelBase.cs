using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBookingSystem.Persistence.Models;

public class ModelBase
{
    [Key]
    public Guid Id { get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
