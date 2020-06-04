using System;

namespace RemoteNotes.UI.Model
{
    public class SaveMemberInfoRequest
    {
        public int MemberId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }  
        
        public DateTime DateOfBirth { get; set; }

        public byte[] Photo { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string Interests { get; set; }
    }
}