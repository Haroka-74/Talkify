﻿namespace Talkify.DTOs.MessageDTOs
{
    public class MessageDTO
    {
        public string SenderId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }
    }
}