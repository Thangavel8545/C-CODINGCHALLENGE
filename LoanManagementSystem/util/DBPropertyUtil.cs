
using System.IO;

namespace LoanManagementSystem.util
{
    public class DBPropertyUtil
    {
        public static string GetConnectionString(string fileName)
        {
            foreach (var line in File.ReadAllLines(fileName))
            {
                if (line.StartsWith("db.connectionString="))
                    return line.Replace("db.connectionString=", "").Trim();
            }
            return null;
        }
    }
}
