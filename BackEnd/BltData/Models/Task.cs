using System;
using System.Collections.Generic;
using System.Text;

namespace BltData.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int? TimeEstimation { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateComplete { get; set; }
        public long? EmployeesAssigned { get; set; }

        public virtual Deliverable Deliverable { get; set; }
    }
}
