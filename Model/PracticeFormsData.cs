using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Access
{
    public partial class PracticeFormsData
    {
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? UserEmail { get; set; } = null;
        public string? GenderChosen { get; set; } = null;
        public string? UserNumber { get; set; } = null;
        public string? MonthPick { get; set; } = null;
        public string? YearPick { get; set; } = null;
        public string? DayPick { get; set; } = null;
        //public string? SubjectsChosen { get; set; }
        public List<string> SubjectsChosenList { get; private set; } = [];
        public string? HobbiesChosen { get; set; } = null;
        public string? CurrentAddress { get; set; } = null;
        public string? StateChosen { get; set; } = null;
        public string? CityChosen { get; set; } = null;
    }
}
