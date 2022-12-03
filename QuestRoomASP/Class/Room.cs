namespace QuestRoomASP.Class
{
    public class Room
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        public string? PassageTime { get; set; }//час проходження
        public int? MinMember { get; set; }
        public int? MaxMember { get; set; }
        public int MinAge { get; set; }
        public string? Adress { get; set; }
       
        public ICollection<Email> Emails { get; set; }
        public string? NameCompany { get; set; }
        public int Rating { get; set; }//рейтинг
        public int FearLevel { get; set; }
        public int DifficultyLevel { get; set; }//рівень складності
        public string? PathLog { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        
        public ICollection<Phone> Phones { get; set; }

        public Room()
        {
            Emails = new List<Email>();
            Pictures= new List<Picture>();
            Phones= new List<Phone>();
        }
    }
}
