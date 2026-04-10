namespace Booking_Service.Application.DTOs.Rooms;

public class CreateRoomRequest
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
}