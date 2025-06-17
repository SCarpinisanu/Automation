using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Automation.Access
{
    public partial class PracticeFormsData
    {
        private XElement? dataSets;

        public PracticeFormsData(int dataSetNumber)
        {
            LoadDataSets(dataSetNumber);
            FirstName = dataSets?.Element("firstName")?.Value;
            LastName = dataSets?.Element("lastName")?.Value;
            UserEmail = dataSets?.Element("userEmail")?.Value;
            GenderChosen = dataSets?.Element("genderChosen")?.Value;
            UserNumber = dataSets?.Element("userNumber")?.Value;
            MonthPick = dataSets?.Element("monthPick")?.Value;
            YearPick = dataSets?.Element("yearPick")?.Value;
            DayPick = dataSets?.Element("dayPick")?.Value;
            //SubjectsChosen = dataSets?.Element("subjectsChosen")?.Value;
            SubjectsChosenList = dataSets?.Elements("subjectsChosen").Select(e => e.Value).ToList() ?? new List<string>();
            HobbiesChosen = dataSets?.Element("hobbiesChosen")?.Value;
            CurrentAddress = dataSets?.Element("currentAddress")?.Value;
        }

        private void LoadDataSets(int dataSetNumber)
        {
            string filePath = Path.Combine("C:\\.NET\\Automation\\Resources\\PracticeFormsData.xml");
            XDocument doc = XDocument.Load(filePath);

            string setName = $"dataSet_{dataSetNumber}";
            dataSets = doc.Root?.Element(setName);
            if (dataSets == null)
            {
                throw new Exception($"Data node '{setName}' not found in the XML file.");
            }
        }

        private string GetDataValue(string setName)
        {
            return dataSets?.Element(setName)?.Value ?? throw new Exception($"Node '{setName}' not found in the data set.");
        }
    }
}
