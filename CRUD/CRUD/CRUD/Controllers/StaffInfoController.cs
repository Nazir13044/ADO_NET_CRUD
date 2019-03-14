using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD.BLL;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class StaffInfoController : Controller
    {
        StaffInfoBLL bll = new StaffInfoBLL();
        // GET: StaffInfo
        public ActionResult Index(string id)
        {
            StaffInfo obj = new StaffInfo();

            ViewData["GenderList"] = bll.GetGenderList();

            if (string.IsNullOrEmpty(id))
            {
                var maxId = bll.GetMaxId();
                obj.StaffPin = maxId;
                obj.StaffName = "";
                obj.GenderID = 0;
                obj.DOB = "";
                obj.IsActive = true;
            }
            else
            {
                var staffInfo = bll.GetInfoById(id);
                obj = staffInfo;
            }
            return View(obj);
        }

        public ActionResult GridDataList()
        {
            var StaffInfoList = bll.PopulateDataGrid();
            return View(StaffInfoList);
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                StaffInfo obj = new StaffInfo();
                obj.StaffPin = collection["StaffPin"];
                obj.StaffName = collection["StaffName"];
                obj.GenderID = Convert.ToInt32(collection["GenderId"]);
                obj.DOB = collection["DOB"];
                obj.IsActive = Convert.ToBoolean(collection["IsActive"].Split(',')[0]);

                int result = 0;
                if (bll.IsExist(obj.StaffPin) > 0)
                {
                    result = bll.Update(obj);
                    if (result > 0)
                    {
                        TempData["notice"] = "Updated data Successfully.  !!!!!";
                    }
                    else
                    {
                        TempData["notice"] = "Failed to Update Data.";
                    }

                }
                else
                {
                    result = bll.Save(obj);

                    if (result > 0)
                    {
                        TempData["notice"] = "Saved Data Successfully. !!!!";
                    }
                    else
                    {
                        TempData["notice"] = "Failed to Save Data.";
                    }



                }

                return RedirectToAction("Index", new { id = DBNull.Value });
            }
            catch
            {
                return View();
            }
        }




        public ActionResult Delete(string id)
        {
            StaffInfo obj = new StaffInfo();
            var staffInfo = bll.GetInfoById(id.PadLeft(8, '0'));
            obj = staffInfo;
            return View(obj);
        }



        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                StaffInfo obj = new StaffInfo();
                obj.StaffPin = collection["StaffPin"];
                int result = bll.Delete(obj.StaffPin);
                if (result > 0)
                {
                    TempData["notice"] = "Deleted data Successfully.  !!!!!";
                }
                else
                {
                    TempData["notice"] = "Failed to Delete Data.";
                }
                return RedirectToAction("Index", new { id = DBNull.Value });
            }
            catch
            {
                return View();
            }
        }
    }
}