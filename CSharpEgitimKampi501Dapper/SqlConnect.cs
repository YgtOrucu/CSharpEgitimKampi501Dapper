using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi501Dapper
{
    public class SqlConnect
    {
        public SqlConnection Baglantı()
        {
            SqlConnection sql = new SqlConnection("Data Source=YIGITORUCU\\MY_YOUTUBE_KURSU;Initial Catalog=DB_CSharpEgitimKampı501Dapper;Integrated Security=True;TrustServerCertificate=True");
            sql.Open();
            return sql;
        }
    }
}
