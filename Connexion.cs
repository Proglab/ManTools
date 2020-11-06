using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ManTools2020
{
    public class Connexion
    {
        private static Connexion instance;
        protected string ConnectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected SqlConnection con;
        protected SqlCommand cmd;

        private Connexion() { }

        public static Connexion Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Connexion();
                }
                return instance;
            }
        }


        public void setQuery(string Query)
        {
            this.con = new SqlConnection(this.ConnectionString);
            this.cmd = new SqlCommand(Query, this.con);
        }

        public void setParam(string key, string value)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = key;
            param.Value = value;
            this.cmd.Parameters.Add(param);
        }

        public DataTable getDataTable()
        {
            this.con.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(this.cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            this.con.Close();
            return dt;
        }

        public string getExecuteScalar()
        {
            this.con.Open();
            string response = cmd.ExecuteScalar().ToString();
            this.con.Close();
            return response;
        }

        public void getExecuteNonQuery()
        {
            this.con.Open();
            cmd.ExecuteNonQuery();
            this.con.Close();
        }
    }
}