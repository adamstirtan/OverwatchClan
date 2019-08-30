using System.Collections.Generic;

using Clan.ObjectModel;

namespace Clan.Web.ViewModels
{
    public class RosterViewModel : BaseViewModel
    {
        public ICollection<Member> Members { get; set; }
    }
}