using System;

namespace Clan.ObjectModel
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartsAt { get; set; }
    }
}