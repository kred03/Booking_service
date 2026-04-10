using Booking_Service.Domain.Common;

namespace Booking_Service.Domain.Entities;

public class Booking : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
}