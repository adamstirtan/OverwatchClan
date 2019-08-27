using System.Collections.Generic;

using GOB.Models;

namespace GOB.ViewModels
{
    public class RosterViewModel : BaseViewModel
    {
        public ICollection<Member> Members { get; set; }
    }
}