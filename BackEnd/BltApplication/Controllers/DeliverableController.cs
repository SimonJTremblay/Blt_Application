using System.Collections.Generic;
using System.Linq;
using BltData;
using BltData.Models;
using Microsoft.AspNetCore.Mvc;

namespace BltApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverableController : ControllerBase
    {
        private readonly IDeliverable _deliverables;
        public DeliverableController(IDeliverable deliverables)
        {
            _deliverables = deliverables;
        }

        // GET api/Deliverable
        [HttpGet]
        public ActionResult<IEnumerable<Deliverable>> Get()
        {
            var deliverableList = _deliverables.GetAll().ToList();
            return deliverableList;
        }

        //// GET api/Deliverable/id
        //[HttpGet("{projectId}")]
        //public ActionResult<IEnumerable<Deliverable>> Get(int projectId)
        //{
        //    var deliverableList = _deliverables.GetDeliverablesFromProject(projectId).ToList();

        //    if (deliverableList == null)
        //    {
        //        return NotFound();
        //    }

        //    return deliverableList;
        //}

        // PUT: api/Deliverable/Id
        [HttpPut("{deliverableId}")]
        public void Put(int deliverableId, Deliverable deliverable)
        {
            _deliverables.Update(deliverableId, deliverable);
        }

        // POST: api/Deliverable
        [HttpPost]
        public void Post(Deliverable deliverable)
        {
            _deliverables.Add(deliverable);
        }

        // Delete api/Deliverable
        [HttpDelete("{deliverableId}")]
        public void Delete(int deliverableId)
        {
            _deliverables.Delete(deliverableId);
        }
    }
}