namespace Booking_Service.Application.DTOs.Bookings;

public class CreateBookingRequest
{
    public Guid UserId { get; set; }
    public Guid RoomId { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
}