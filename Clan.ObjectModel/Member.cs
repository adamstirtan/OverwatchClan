﻿namespace Clan.ObjectModel
{
    public class Member : BaseEntity
    {
        public string Name { get; set; }
        public string BattleTag { get; set; }
        public string ImageUrl { get; set; }
        public bool Active { get; set; }
    }
}