namespace QuestRoomASP.Class
{
    public class Picture
    {
        public int Id { get; set; }
        public int RoomsId { get; set; }
        public virtual Room Rooms { get; set; }
        public string? PathImg { get; set; }
    }
}
