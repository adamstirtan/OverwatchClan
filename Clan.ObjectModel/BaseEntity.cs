using System;

namespace Clan.ObjectModel
{
    public abstract class BaseEntity
    {
        private long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}