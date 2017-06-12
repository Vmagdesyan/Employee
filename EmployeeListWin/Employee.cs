namespace EmployeeListWin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        public int Id { get; set; }

        public string FIO { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? DateInPost { get; set; }

        public string Department { get; set; }

        public string EMail { get; set; }

        public string TelNumber { get; set; }

        public int? PositionId { get; set; }

        public virtual Position Position { get; set; }
    }
}
