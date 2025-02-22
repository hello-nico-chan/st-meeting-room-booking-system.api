using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBookingSystem.Persistence.Models;

public class ModelBase
{
    [Key]
    public Guid Id { get; set;}

    public DateTime createdAt { get; set; } = DateTime.Now;

    public DateTime updatedAt { get; set; } = DateTime.Now;
}
