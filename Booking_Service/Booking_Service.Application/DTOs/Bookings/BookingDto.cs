namespace Booking_Service.Application.DTOs.Bookings;

public class BookingDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    public Guid RoomId { get; set; }
    public string RoomName { get; set; } = string.Empty;

    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
}