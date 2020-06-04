using Newtonsoft.Json;
using System;
using System.IO;
using Xamarin.Forms;

namespace RemoteNotes.UI.Model
{
    public class Note
    {
        public int Id { get; set; }

        public string Topic { get; set; }
        
        public string Text { get; set; }
        
        public byte[] Photo { get; set; }
        
        public DateTime PublishTime { get; set; }
        
        public DateTime ModifyTime { get; set; }
        
        public int MemberId { get; set; }

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