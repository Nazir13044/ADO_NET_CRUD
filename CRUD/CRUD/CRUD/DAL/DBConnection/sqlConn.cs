using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD.DAL.DBConnection
{
    public class sqlConn
    {
        SqlConnection con = null;
        public sqlConn()
        {
            con = getConn();
        }

        public SqlConnection getConn()
        {
            string connection = "";
            connection = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            return con;
        }

        public void Open()
        {
            if(con.State==ConnectionState.Closed)
                con.Open();
        }

        public void Close()
        {
            if(con.State==ConnectionState.Open)
                con.Close();
        }

        internal DataTable GetDataThroughDataTable(string query,List<SqlParameter> param)
        {
            DataTable dt =new DataTable();
            try
            {
                if(con.State==ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query,con);
                if (param != null)
                {
                    cmd.Parameters.AddRange(param.ToArray());
                }
                SqlDataAdapter dataObj = new SqlDataAdapter(cmd);
                dataObj.Fill(dt);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if(con.State==ConnectionState.Open)
                    con.Close();
            }

            return dt;
        }

        public string GetSingleString(string query,List<SqlParameter> param)
        {
            DataTable dt = new DataTable();
            dt = GetDataThroughDataTable(query, param);
            return dt.Rows.Count > 0 ? Convert.ToString(dt.Rows[0][0]) : "";
        }

        public int GetSingleInt(string query, List<SqlParameter> param)
        {
            DataTable dt = new DataTable();
            dt = GetDataThroughDataTable(query, param);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }

        internal int ExecuteNonQuery(string query,List<SqlParameter> param)
        {
            int success = 0;
            try
            {
                if(con.State==ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddRange(param.ToArray());
                success = (int) cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                if(con.State==ConnectionState.Open)
                    con.Close();
            }

            return success;
        }

        internal int GetCount(String query, List<SqlParameter> param)
        {
            DataTable dt = new DataTable();
            dt = GetDataThroughDataTable(query, param);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows.Count) : 0;

        }
    }
}