using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD.DAL;
using CRUD.Models;

namespace CRUD.BLL
{

    public class StaffInfoBLL
    {
        StaffInfoDAL dal = new StaffInfoDAL();

        public string GetMaxId()
        {
            return dal.GetMaxId();
        }

        public int IsExist(string pin)
        {
            return dal.IsExist(pin);
        }

        public int Save(StaffInfo Obj)
        {
            return dal.Save(Obj);
        }

        public int Update(StaffInfo Obj)
        {
            return dal.Update(Obj);
        }

        internal int Delete(string pin)
        {
            return dal.Delete(pin);
        }

        internal IEnumerable<StaffInfo> PopulateDataGrid()
        {
            return dal.PopulateDataGrid();
        }

        public StaffInfo GetInfoById(string pin)
        {
            return dal.GetInfoById(pin);
        }

        public IEnumerable<SelectListItem> GetGenderList()
        {
            try
            {
                var GenderList = dal.GetAllGenders().Select(o => new SelectListItem { 
                    Value=Convert.ToString(o.GenderID),
                    Text = o.GenderName
                    }).ToList();
                return GenderList;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
        }
    }
}