using BltApplication.Models;
using BltData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BltData
{
    public interface IDeliverable
    {
        IEnumerable<Deliverable> GetAll();
        Deliverable GetById(int id);
        void Add(Deliverable deliverable);
        void Delete(int deliverableId);
        void Update(int id, Deliverable deliverable);
    }
}
