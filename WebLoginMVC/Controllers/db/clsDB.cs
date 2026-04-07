using Microsoft.Data.SqlClient;
using System.Data;

namespace WebLoginMVC.Controllers.db
{
    public class clsDB
    {
        readonly string CadenaConexion;
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;

        public clsDB()
        {
            CadenaConexion = "Data Source=localhost;Initial Catalog=ExLogin;Integrated Security=True";
            cn = new SqlConnection(CadenaConexion);
            cmd = new SqlCommand("", cn);
            da = new SqlDataAdapter(cmd);
        }

        public DataTable getDataTable() {
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void Sentencia(string sql)
        {
            cmd.CommandText = sql;
            cmd.Parameters.Clear();
        }

        public string[] getRegistro()
        {
            DataTable dt = getDataTable();
            if (dt.Rows.Count == 0) return null;

            return System.Array.ConvertAll(dt.Rows[0].ItemArray, x => x.ToString());
        }

        public string[][] getRegistros()
        {
            DataTable dt = getDataTable();
            if (dt.Rows.Count == 0) return null;

            int i = 0;
            string[][] registros = new string[dt.Rows.Count][];

            foreach(DataRow dr in dt.Rows)
                registros[i++] = System.Array.ConvertAll(dr.ItemArray, x => x.ToString());

            return registros;
        }
    }
}
