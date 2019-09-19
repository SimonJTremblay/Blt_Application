using BltData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BltApplication.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name {get;set;}
        public string Description {get;set;}
        public int Lead {get;set;}
        public virtual IEnumerable<Deliverable> DeliverableList { get; set; }
    }
}
