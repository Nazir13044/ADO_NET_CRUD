using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUD.DAL;
using CRUD.Models;

namespace CRUD.BLL
{
    public class GenderBLL
    {
        GenderDAL dal = new GenderDAL();

        public int GetMaxID()
        {
            return dal.GetMaxID();
        }

        public int IsExist(int id)
        {
            return dal.IsExist(id);
        }

        public int Save(Gender obj)
        {
            return dal.Save(obj);
        }

        public int Update(Gender obj)
        {
            return dal.Update(obj);
        }

        internal int Delete(int id)
        {
            return dal.Delete(id);
        }

        internal IEnumerable<Gender> PopulateDataGrid()
        {
            return dal.PopulateDataGrid();
        }

        public Gender GetInfoByID(int id)
        {
            return dal.GetInfoByID(id);
        }

        public int CheckAlreadyExist(int id)
        {
            return dal.CheckAlreadyExist(id);
        }
    }
}