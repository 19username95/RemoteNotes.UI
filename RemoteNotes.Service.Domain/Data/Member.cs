using Newtonsoft.Json;
using System;
using System.IO;
using Xamarin.Forms;

namespace RemoteNotes.Service.Domain.Data
{
    public class Member
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Interests { get; set; }
        public byte[] Photo { get; set; }
        public DateTime ModifyTime { get; set; }
        
        public int AccessLevel { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public ImageSource PhotoSource => GetImageSource();

        private ImageSource GetImageSource()
        {
            var image = default(ImageSource);

            if (Photo != null)
            {
                image = ImageSource.FromStream(() => new MemoryStream(Photo));
            }

            return image;
        }
    }
}