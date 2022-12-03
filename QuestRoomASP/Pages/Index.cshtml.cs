using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuestRoomASP.Class;

namespace QuestRoomASP.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        QuestRoomContext context;
        


        public List<Room> rooms { get;  set; } = new();
        public List<Email> emails { get;  set; } = new();
        public List<Phone> phones { get;  set; } = new();
        public List<Picture> pictures { get;  set; } = new();
        public IndexModel(QuestRoomContext db)
        {
            context = db;
        }
        public void OnGet()
        {
            rooms = context.Rooms.AsNoTracking().ToList();
            emails = context.Emails.AsNoTracking().ToList();
            phones = context.Phones.AsNoTracking().ToList();
            pictures = context.Pictures.AsNoTracking().ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var room=await context.Rooms.FindAsync(id);
            if (room!=null)
            {
                context.Rooms.Remove(room);
                await context.SaveChangesAsync();   
            }
            else
            {
               return NotFound(new { message = "Комната не найден" });
            }
            return RedirectToPage();

        }
    }
}