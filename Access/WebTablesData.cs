using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Automation.Access
{
    public partial class WebTablesData
    {
        private XElement? dataNode;

        public WebTablesData(int dataSetNumber)
        {
            LoadDataNode(dataSetNumber);
            FirstName = GetDataValue("firstName");
            LastName = GetDataValue("lastName");
            UserEmail = GetDataValue("userEmail");
            Age = GetDataValue("age");
            Salary = GetDataValue("salary");
            Department = GetDataValue("department");
        }

        private void LoadDataNode(int dataSetNumber)
        {
            string filePath = Path.Combine("C:\\.NET\\Automation\\Resources\\WebTablesData.xml");
            XDocument doc = XDocument.Load(filePath);

            string nodeName = $"dataSet_{dataSetNumber}";
            dataNode = doc.Root?.Element(nodeName);
            if (dataNode == null)
            {
                throw new Exception($"Data node '{nodeName}' not found in the XML file.");
            }
        }

        private string GetDataValue(string nodeName)
        {
            return dataNode?.Element(nodeName)?.Value ?? throw new Exception($"Node '{nodeName}' not found in the data set.");
        }
    }
}
