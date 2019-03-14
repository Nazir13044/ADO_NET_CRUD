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
    public class StaffInfoDAL
    {
        sqlConn con = new sqlConn();

        public string GetMaxId()
        {
            string squey = " Select ISNULL(Max(StaffPin),0)+1 from StaffInfo  ";
            string MaxId = con.GetSingleString(squey, null);
            return MaxId.PadLeft(8, '0');
        }

        public int IsExist(string pin)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<SqlParameter> param = new List<SqlParameter>
            {
                new SqlParameter("@StaffPin", pin)
            };
            query.Append("Select COUNT(*) from StaffInfo where StaffPin=@StaffPin ");
            return con.GetSingleInt(query.ToString(), param);
        }

        public int Save(StaffInfo Obj)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<SqlParameter> param = new List<SqlParameter>
            {
                new SqlParameter("@StaffPin", Obj.StaffPin.Trim()),
                new SqlParameter("@StaffName", Obj.StaffName.Trim()),
                new SqlParameter("@GenderID", Obj.GenderID),
                new SqlParameter("@DOB", Obj.DOB.Trim()),
                new SqlParameter("@IsActive", Obj.IsActive)
            };
            query.Append(" Insert into StaffInfo(StaffPin, StaffName,GenderID, DOB, IsActive) values(@StaffPin, @StaffName,@GenderId, @DOB, @IsActive) ");
            return con.ExecuteNonQuery(query.ToString(), param);
        }

        public int Update(StaffInfo Obj)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<SqlParameter> param = new List<SqlParameter>
            {
                new SqlParameter("@StaffPin", Obj.StaffPin.Trim()),
                new SqlParameter("@StaffName", Obj.StaffName.Trim()),
                new SqlParameter("@GenderID", Obj.GenderID),
                new SqlParameter("@DOB", Obj.DOB.Trim()),
                new SqlParameter("@IsActive", Obj.IsActive)
            };
            query.Append(" Update StaffInfo set StaffName=@StaffName, GenderID=@GenderID, DOB=@DOB, IsActive=@IsActive where StaffPin=@StaffPin ");
            return con.ExecuteNonQuery(query.ToString(), param);
        }

        internal int Delete(string pin)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<SqlParameter> param = new List<SqlParameter>
            {
                new SqlParameter("@StaffPin", pin)
            };
            query.Append(" If  Exists( Select Count(*)  from StaffInfo where StaffPin=@StaffPin)");
            query.Append(" begin");
            query.Append(" Delete from StaffInfo  where StaffPin=@StaffPin");
            query.Append(" End");
            return con.ExecuteNonQuery(query.ToString(), param);
        }

        internal IEnumerable<StaffInfo> PopulateDataGrid()
        {
            string query = " Select a.StaffPin, a.StaffName, a.DOB, a.IsActive ,b.GenderID,b.GenderName from StaffInfo a Inner join Gender b ON a.GenderID=b.GenderID ";
            var dt = con.GetDataThroughDataTable(query, null);
            return from DataRow row in dt.Rows select StaffInfo.ConvertToStaffInfo(row);
        }

        public StaffInfo GetInfoById(string pin)
        {
            string query = "";

            List<SqlParameter> param = new List<SqlParameter>
            {
                new SqlParameter("@StaffPin",pin )
            };

            query = " Select a.StaffPin, a.StaffName, a.DOB, a.IsActive ,b.GenderID,b.GenderName from StaffInfo a inner join Gender b ON a.GenderID=b.GenderID  where a.StaffPin=@StaffPin";
            var dt = con.GetDataThroughDataTable(query, param);
            return StaffInfo.ConvertToStaffInfo(dt.Rows[0]);
        }

        public IEnumerable<Gender> GetAllGenders()
        {
            try
            {
                con.Open();
                string query = " Select GenderID,RTrim(LTrim(GenderID))+'-'+GenderName  GenderName from Gender  ";
                var dt = con.GetDataThroughDataTable(query, null);
                return from DataRow row in dt.Rows select Gender.ConvertToGender(row);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }

        }
    }
}