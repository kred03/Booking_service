namespace Booking_Service.Application.DTOs.Rooms;

public class UpdateRoomRequest
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
}