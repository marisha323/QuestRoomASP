using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestRoomASP.Class;
using System.Globalization;

namespace QuestRoomASP.Pages
{

    [IgnoreAntiforgeryToken]
    public class EditModel : PageModel
    {
        QuestRoomContext context;
        [BindProperty]
        public Room? room { get; set; }
        [BindProperty]
        public Picture? picture { get; set; }
        [BindProperty]
        public Phone? phone { get; set; }
        [BindProperty]
        public Email? email { get; set; }
        public EditModel(QuestRoomContext db)
        {
            context = db;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            room = context.Rooms.Where(o=>o.Id==id).FirstOrDefault();
            phone=context.Phones.Where(o=>o.RoomsId==room.Id).FirstOrDefault();
            email = context.Emails.Where(o => o.RoomsId == room.Id).FirstOrDefault();
            picture = context.Pictures.Where(o => o.RoomsId == room.Id).FirstOrDefault();

            if (room == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //int id,string PathImg,string PhoneNumber,string NewEmail
            //room = context.Rooms.Where(o => o.Id == o.Id).FirstOrDefault();
            //phone = context.Phones.Where(o => o.RoomsId == room.Id).FirstOrDefault();
            //email = context.Emails.Where(o => o.RoomsId == room.Id).FirstOrDefault();
            //picture = context.Pictures.Where(o => o.RoomsId == room.Id).FirstOrDefault();

            //if (room == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    //room.Name = Name;
            //    //room.Info = Info;
            //    //room.PassageTime = PassageTime;
            //    //room.MinMember = MinMember;
            //    //room.MaxMember = MaxMember;
            //    //room.MinAge = MinAge;   
            //    //room.Adress = Adress;
            //    //room.NameCompany= NameCompany;
            //    //room.Rating = Rating;
            //    //room.FearLevel = FearLevel;
            //    //room.DifficultyLevel = DifficultyLevel;
            //    //room.PathLog = PathLog;
            //    //email.NewEmail= Emails;
            //    //phone.PhoneNumber = Phones;
            //    //picture.PathImg= Pictures;
            //}

            IFormFileCollection files = Request.Form.Files;

            var passrom = $@"{Directory.GetCurrentDirectory()}\wwwroot\Img\{room.Name.Replace(" ", "-")}";
            Directory.CreateDirectory(passrom);
            room.PathLog = $@"{passrom}\{files[0].FileName}";
            using (var fs = new FileStream(room.PathLog, FileMode.Create))
            {
                await files[0].CopyToAsync(fs);
            }
            room.PathLog = room.PathLog.Split("wwwroot")[1];

            context.Update(room!);

            await context.SaveChangesAsync();

            for (int i = 1; i < files.Count; i++)
            {
                picture = new Picture();
                picture.RoomsId = context.Rooms.Where(o => o.Name == room.Name).FirstOrDefault().Id;
                var path = $@"{Directory.GetCurrentDirectory()}\wwwroot\Img\{room.Name.Replace(" ", "-")}";
                Directory.CreateDirectory(path);
                picture.PathImg = $@"{path}\{files[i].FileName}";

                using (var fs = new FileStream(picture.PathImg, FileMode.Create))
                {
                    await files[i].CopyToAsync(fs);
                }
                picture.PathImg = picture.PathImg.Split("wwwroot")[1];
                context.Update(picture!);



            }

            context.Rooms.Update(room!);
            await context.SaveChangesAsync();

            context.Emails.Update(email!);
            context.Phones.Update(phone!);
            context.Pictures.Update(picture!);
            //context.Pictures.Where(o => o.RoomsId == o.Id).First().PathImg = PathImg;

            //context.Phones.Where(o => o.RoomsId == o.Id).First().PhoneNumber = PhoneNumber;

            //context.Emails.Where(o => o.RoomsId == o.Id).First().NewEmail = NewEmail;
            await context.SaveChangesAsync();


            //Room room2 = context.Rooms.Where(o=>o.Id==id).FirstOrDefault();

           

            //phone.RoomsId = context.Rooms.Where(o => o.Name == room2.Name).FirstOrDefault().Id;
            //context.Update(phone);
            //email.RoomsId = context.Rooms.Where(o => o.Name == room2.Name).FirstOrDefault().Id;
            //context.Update(email);

            //await context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
