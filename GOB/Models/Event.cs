using System;

namespace GOB.Models
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartsAt { get; set; }
    }
}