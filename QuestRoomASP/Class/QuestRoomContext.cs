using Microsoft.EntityFrameworkCore;

namespace QuestRoomASP.Class
{
    public class QuestRoomContext:DbContext
    {
        public DbSet<Email> Emails { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public QuestRoomContext(DbContextOptions<QuestRoomContext> options) :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
