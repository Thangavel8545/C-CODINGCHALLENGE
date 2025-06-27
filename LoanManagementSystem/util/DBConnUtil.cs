
using System.Data.SqlClient;

namespace LoanManagementSystem.util
{
    public class DBConnUtil
    {
        public static SqlConnection GetConnection(string fileName)
        {
            string connStr = DBPropertyUtil.GetConnectionString(fileName);
            return new SqlConnection(connStr);
        }
    }
}
