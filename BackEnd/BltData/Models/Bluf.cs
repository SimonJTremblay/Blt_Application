using System;
using System.Collections.Generic;
using System.Text;

namespace BltData.Models
{
    public class Bluf
    {
        public int BlufId { get; set; }
        public int DeliverableId { get; set; }
        public int Schedule { get; set; }
        public int Budget { get; set; }
        public int Scope { get; set; }
        public int OtherRisks { get; set; }
        public int Issues { get; set; }
        public DateTime Date { get; set; }
        public virtual Deliverable Deliverable { get; set; }

    }
}
