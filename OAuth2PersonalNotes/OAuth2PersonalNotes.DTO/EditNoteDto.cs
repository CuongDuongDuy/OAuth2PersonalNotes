using System;

namespace OAuth2PersonalNotes.DTO
{
    public class EditNoteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}
