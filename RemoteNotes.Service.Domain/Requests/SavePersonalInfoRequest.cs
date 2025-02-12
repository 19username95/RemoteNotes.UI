using System;

namespace RemoteNotes.Service.Domain.Requests
{
    public class SavePersonalInfoRequest
    {
        public int MemberId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime ModifyTime { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}