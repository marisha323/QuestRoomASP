namespace QuestRoomASP.Class
{
    public class Email
    {
        public int Id { get; set; }
        public string? NewEmail { get; set; }

        public int RoomsId { get; set; }
        public virtual Room Rooms { get; set; }
    }
}
