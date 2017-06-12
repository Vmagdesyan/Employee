using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeList.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FIO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateInPost { get; set; }

        public string Department { get; set; }

        public string EMail { get; set; }

        public string TelNumber { get; set; }

        public int? PositionId { get; set; }

        public Position Position { get; set; }

    }
}