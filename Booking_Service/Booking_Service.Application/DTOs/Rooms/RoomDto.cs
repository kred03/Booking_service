namespace Booking_Service.Application.DTOs.Rooms;

public class RoomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
}