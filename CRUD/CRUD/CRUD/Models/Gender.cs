using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class Gender
    {
        [Required]
        [Display(Name = "Gender ID :")]
        public int GenderID { get; set; }

        [Required]
        [Display(Name = "Gender Name :")]
        public string GenderName { get; set; }

        public static Gender ConvertToGender(DataRow row)
        {
            return new Gender
            {
                GenderID=row.Table.Columns.Contains("GenderID") ? Convert.ToInt32(row["GenderID"]) : 0,
                GenderName=row.Table.Columns.Contains("GenderName") ? Convert.ToString(row["GenderName"]) : "",
            };
        }
    }
}