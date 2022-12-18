using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestRoomASP.Class;

namespace QuestRoomASP.Pages
{
    [IgnoreAntiforgeryToken]
    public class AddRoomModel : PageModel
    {
        QuestRoomContext context;
      
        [BindProperty]
        public Room room { get; set; }
        [BindProperty]
        public Picture picture  { get; set; }
        [BindProperty]
        public Phone phone { get; set; }
        [BindProperty]
        public Email email { get; set; }
        public AddRoomModel(QuestRoomContext db)
        {
            context = db;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            IFormFileCollection files = Request.Form.Files;

            var passrom = $@"{Directory.GetCurrentDirectory()}\wwwroot\Img\{room.Name.Replace(" ", "-")}";
            Directory.CreateDirectory(passrom);
            room.PathLog = $@"{passrom}\{files[0].FileName}";
            using (var fs = new FileStream(room.PathLog, FileMode.Create))
            {
                await files[0].CopyToAsync(fs);
            }
            room.PathLog = room.PathLog.Split("wwwroot")[1];

            await context.AddAsync(room);
           
            await context.SaveChangesAsync();
            
            for (int i = 1; i < files.Count; i++)
            {
                picture=new Picture();
                picture.RoomsId = context.Rooms.Where(o => o.Name == room.Name).FirstOrDefault().Id;
                var path = $@"{Directory.GetCurrentDirectory()}\wwwroot\Img\{room.Name.Replace(" ","-")}";
                Directory.CreateDirectory(path);
                picture.PathImg = $@"{path}\{files[i].FileName}";

                using (var fs=new FileStream(picture.PathImg,FileMode.Create))
                {
                    await files[i].CopyToAsync(fs);
                }
                picture.PathImg = picture.PathImg.Split("wwwroot")[1];
                await context.AddAsync(picture);


                using (var fs = new FileStream(picture.PathImg, FileMode.Create))
                {
                    await files[i].CopyToAsync(fs);
                }
                picture.PathImg = picture.PathImg.Split("wwwroot")[1];
                await context.AddAsync(picture);
            }
           
            phone.RoomsId = context.Rooms.Where(o => o.Name == room.Name).FirstOrDefault().Id;
            context.Add(phone);
            email.RoomsId = context.Rooms.Where(o => o.Name == room.Name).FirstOrDefault().Id;
            context.Add(email);
           await context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
