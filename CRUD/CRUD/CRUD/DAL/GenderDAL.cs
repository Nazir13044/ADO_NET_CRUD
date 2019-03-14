using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using CRUD.DAL.DBConnection;
using CRUD.Models;

namespace CRUD.DAL
{
    public class GenderDAL
    {
        sqlConn con =new sqlConn();

        public int GetMaxID()
        {
            string query = " Select ISNULL(Max(GenderID),0)+1 from Gender ";
            int maxId = con.GetSingleInt(query, null);
            return maxId;
        }

        public int IsExist(int id)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<SqlParameter> param = new List<SqlParameter>
            {
                new SqlParameter("@GenderID",id)
            };
            query.Append(" Select Count(*)  from Gender where GenderID=@GenderID ");
            return con.GetSingleInt(query.ToString(), param);
        }

        public int Save (Gender obj)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<SqlParameter> param = new List<SqlParameter>
            {
                new SqlParameter("@GenderID",obj.GenderID),
                new SqlParameter("@GenderName",obj.GenderName)
            };
            query.Append(" Insert into Gender(GenderID,GenderName) Values (@GenderID,@GenderName) ");
            return con.ExecuteNonQuery(query.ToString(), param);
        }

        public int Update(Gender obj)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<SqlParameter> param= new List<SqlParameter>
            {
                new SqlParameter("@GenderID",obj.GenderID),
                new SqlParameter("@GenderName",obj.GenderName)
            };
            query.Append("Update Gender set GenderName=@GenderName where GenderID=@GenderID");
            return con.ExecuteNonQuery(query.ToString(), param);
        }

        internal int Delete(int id)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<SqlParameter> param = new List<SqlParameter>
            {
                new SqlParameter("@GenderID",id)
            };
            query.Append(" If Exists(Select Count(*) from Gender where GenderID=@GenderID) ");
            query.Append(" begin ");
            query.Append(" Delete from Gender where GenderID=@GenderID");
            query.Append(" End ");
            return con.ExecuteNonQuery(query.ToString(), param);
        }

        internal IEnumerable<Gender> PopulateDataGrid()
        {
            string query = "";
            query=" Select GenderID,GenderName from Gender ";
            var dt = con.GetDataThroughDataTable(query, null);
            return from DataRow row in dt.Rows select Gender.ConvertToGender(row);
        }

        public Gender GetInfoByID(int id)
        {
            string query = "";
            List<SqlParameter> param =new List<SqlParameter>
            {
                new SqlParameter("@GenderID",id)
            };
            query = " Select GenderID,GenderName  from Gender where  GenderID=@GenderID";
            var dt = con.GetDataThroughDataTable(query, param);
            return Gender.ConvertToGender(dt.Rows[0]);
        }

        public int CheckAlreadyExist(int id)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<SqlParameter> param = new List<SqlParameter>
            {
                new SqlParameter("@GenderID",id)
            };
            query.Append(" Select a.* from StaffInfo a where a.GenderID=@GenderID ");
            return con.GetCount(query.ToString(), param);
        }
    }
}