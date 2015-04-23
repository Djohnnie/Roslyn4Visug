using System;
using System.Data.SqlClient;
using System.IO;

namespace Roslyn.Visug.NewCSharpFeatures.ExceptionFilters
{
    // http://www.volatileread.com/Wiki/Index?id=1087

    class Program
    {
        static void Main(string[] args)
        {

            String fileName = "fakeFile.dat";

            try
            {
                File.Open(fileName, FileMode.Open);
            }
            catch (FileNotFoundException ex) when (ex.FileName.Contains(fileName))
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();

            try
            {

            }
            catch (SqlException ex) when (ex.Number == 25)
            {

            }
        }
    }
}
