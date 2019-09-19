using BltApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BltData.Models
{
    public class Deliverable
    {
        public int DeliverableId { get; set; }
        public string Heading { get; set; }
        public int Priority { get; set; }
        public int? Lead { get; set; }
        public DateTime? DateScheduledStart { get; set; }
        public int? TimeEstimation { get; set; }
        public virtual List<int> ProjectIdList { get; set; }
        public virtual IEnumerable<Task> TaskList { get; set; }
    }
}
