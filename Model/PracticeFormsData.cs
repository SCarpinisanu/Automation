using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Access
{
    public partial class PracticeFormsData
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserEmail { get; set; }
        public string? GenderChosen { get; set; }
        public string? UserNumber { get; set; }
        public string? MonthPick { get; set; }
        public string? YearPick { get; set; }
        public string? DayPick { get; set; }
        //public string? SubjectsChosen { get; set; }
        public List<string> SubjectsChosenList { get; private set; } = new List<string>();
        public string? HobbiesChosen { get; set; }
        public string? CurrentAddress { get; set; }

    }
}
