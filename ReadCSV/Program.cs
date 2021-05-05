using GenericParsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCSV
{
    class Program
    {
        static void Main(string[] args)
        {

            //string filePath = "../DataLoader/FileProcessedOn04-13-2021@00_51_12.csv";

            //DataSet dsResult;

            //using (GenericParserAdapter parser = new GenericParserAdapter("../DataLoader/FileProcessedOn04-13-2021@00_51_12.csv"))
            //{
            //    parser.Load("MyData.xml");
            //    dsResult = parser.GetDataSet();
            //}

            //Console.WriteLine(dsResult);




            //var myDataTable = GetDataTableFromCsv(filePath, true);

            LoadCSVLinebyLine();

            Console.ReadLine();

        }


        static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
        {
            string header = isFirstRowHeader ? "Yes" : "No";

            string pathOnly = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);

            string sql = @"SELECT * FROM [" + fileName + "]";

            using (OleDbConnection connection = new OleDbConnection(
                      @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                      ";Extended Properties=\"Text;HDR=" + header + "\""))
            using (OleDbCommand command = new OleDbCommand(sql, connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                dataTable.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        private static void LoadCSVLinebyLine()
        {
            string filePath = "../DataLoader/FileProcessedOn04-13-2021@00_51_12.csv";

            if (File.Exists(filePath))
            {
                DataTable dataTable = new DataTable();
                DataRow workRow;
                DataColumn column;
                // Create new DataColumn, set DataType, ColumnName and add to DataTable.
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "JOIN_File_Unique_Key";
                dataTable.Columns.Add(column);

                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "PRISM_File_Number";
                dataTable.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "dataID";
                dataTable.Columns.Add(column);

                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "url";
                dataTable.Columns.Add(column);


                using (StreamReader sr = new StreamReader(filePath))
                {
                    string currentLine;
                    // currentLine will be null when the StreamReader reaches the end of file
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        string[] array = currentLine.Split(',');
                        //Console.WriteLine("first line is {0}, second is {1}, thrid is {2}.", array[0], array[1], array[2], array[3]);
                        workRow = dataTable.NewRow();
                        workRow[0] = array[0];
                        workRow[1] = array[1];
                        workRow[2] = array[2];
                        workRow[3] = array[3];
                        dataTable.Rows.Add(workRow);
                    }
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Console.WriteLine("This is JOIN file key " + dataRow[0] + dataRow[1] + dataRow[2] + dataRow[3]);

                    Console.ReadLine();
                }


                //File.Delete(filePath);
            }
        }
    }
}
