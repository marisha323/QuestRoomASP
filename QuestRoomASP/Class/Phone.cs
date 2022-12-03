namespace QuestRoomASP.Class
{
    public class Phone
    {
        public int Id { get; set; }
        public string? PhoneNumber { get; set; }

        public int RoomsId { get; set; }
        public virtual Room Rooms { get; set; }
    }
}
