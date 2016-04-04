using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;


namespace ScoreEngine
{
    class Program
    {

        static void Main(string[] args)
        {
            string csvFile = "event-log-03312016.csv";
            using (var csvReader = new StreamReader(@csvFile))
            {
                var csv = new CsvReader(csvReader);
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.IgnoreHeaderWhiteSpace = true;
                csv.Configuration.SkipEmptyRecords = true;
                csv.Configuration.TrimFields = true;
                csv.Configuration.IsHeaderCaseSensitive = false;

                List<string> myStringColumn = new List<string>();
                using (var fileReader = File.OpenText(csvFile))
                using (var csvResult = new CsvHelper.CsvReader(fileReader))
                {
                    while (csvResult.Read())
                    {
                        string intField = csvResult.GetField<string>("contact id");
                        myStringColumn.Add(intField);

                        string stringField = csvResult.GetField<string>("event");
                        myStringColumn.Add(stringField);

                        string decimalField = csvResult.GetField<string>("score");
                        myStringColumn.Add(decimalField);

                    }
                }
                Console.WriteLine("reading");

                var entry = from n in myStringColumn select n;

                foreach(var member in entry)
                    Console.WriteLine(": {0}", member);

                Console.ReadKey();

            }
        }
    }
}
