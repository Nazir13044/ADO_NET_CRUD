using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using CRUD.BLL;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class GenderController : Controller
    {
        GenderBLL bll = new GenderBLL();
        // GET: Gender
        public ActionResult Index(int? id)
        {
            Gender gender= new Gender();
            if (id < 1 || id == null)
            {
                var maxid = bll.GetMaxID();
                gender.GenderID = maxid;
                gender.GenderName = "";
            }
            else
            {
                var genderInfo = bll.GetInfoByID(Convert.ToInt32(id));
                gender = genderInfo;
            }
            return View(gender);
        }
        public ActionResult GridDataList()
        {
            var genderList = bll.PopulateDataGrid();
            return View(genderList);
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                Gender gender = new Gender();
                gender.GenderID = Convert.ToInt32(collection["GenderID"]);
                gender.GenderName = collection["GenderName"];

                int result = 0;
                if (bll.IsExist(gender.GenderID) > 0)
                {
                    result = bll.Update(gender);
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
                    result = bll.Save(gender);

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

        public ActionResult Delete(int id)
        {
            Gender gender = new Gender();
            var genderInfo = bll.GetInfoByID(Convert.ToInt32(id));
            gender = genderInfo;
            return View(gender);
        }

        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                Gender gender = new Gender();
                gender.GenderID = Convert.ToInt32(collection["GenderID"]);
                
                int exist = bll.CheckAlreadyExist(gender.GenderID);
                if (exist < 1)
                {
                    int result = bll.Delete(gender.GenderID);

                    if (result > 0)
                    {
                        TempData["notice"] = "Deleted data Successfully.  !!!!!";
                    }
                    else
                    {
                        TempData["notice"] = "Failed to Delete Data.";
                    }
                    
                }
                else
                {
                    TempData["notice"] = "Failed to Delete Data.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}