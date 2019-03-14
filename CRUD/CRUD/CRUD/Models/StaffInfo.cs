using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class StaffInfo
    {
        [Required]
        [Display(Name = "Staff Pin :")]
        public string StaffPin { get; set; }

        [Required]
        [Display(Name = "Staff Name :")]
        public string StaffName { get; set; }

        public string GenderName { get; set; }

        [Required]
        [Display(Name = "Gender :")]
        public int GenderID { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public string DOB { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public virtual ICollection<Gender> gender { get; set; }

        public static StaffInfo ConvertToStaffInfo (DataRow row)
        {
            return new StaffInfo
            {
                StaffPin = row.Table.Columns.Contains("StaffPin") ? Convert.ToString(row["StaffPin"]) : "",
                StaffName = row.Table.Columns.Contains("StaffName") ? Convert.ToString(row["StaffName"]) : "",
                GenderID = row.Table.Columns.Contains("GenderID") ? Convert.ToInt32(row["GenderID"]) : 0,
                GenderName = row.Table.Columns.Contains("GenderName") ? Convert.ToString(row["GenderName"]) : "",
                DOB=row.Table.Columns.Contains("DOB") ? Convert.ToDateTime(row["DOB"]).ToString("dd/MMM/yyyy") : "",
                IsActive = row.Table.Columns.Contains("IsActive") ? Convert.ToBoolean(row["IsActive"]) : false,
            };
        }

    }
}