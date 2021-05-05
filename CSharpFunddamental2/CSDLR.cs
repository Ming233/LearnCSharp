using csdlr;
using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace CSharpFunddamental2
{
    public class CSDLR
    {
        public void CSharpDLR()
        {
            //AutomateExcel won't work under the WPS
            //AutomateExcel();


            Console.WriteLine("Read ReadXmlExpando");
            ReadXmlExpando();

        }

        public static void AutomateExcel()
        {
            Type excelType = Type.GetTypeFromProgID("Excel.Application");
            dynamic excel = Activator.CreateInstance(excelType);
            excel.Visible = true;

            excel.Workbooks.Add();

            dynamic sheet = excel.ActiveSheet;

            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                sheet.Cells[i + 1, "A"] = processes[i].ProcessName;
                sheet.Cells[i + 1, "B"] = processes[i].Threads.Count;
            }
        }
        private static void ReadXmlExpando()
        {
            var doc = XDocument.Load("Employees.xml").AsExpando();
            foreach (var employee in doc.Employees)
            {
                Console.WriteLine(employee.FirstName);
            }
        }

    }


}
