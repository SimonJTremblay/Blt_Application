using BltData;
using BltData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BltApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlufController : ControllerBase
    {
        private readonly IBluf _blufs;
        public BlufController(IBluf blufs)
        {
            _blufs = blufs;
        }

        // POST: api/Bluf
        [HttpPost]
        public void Post(Bluf bluf)
        {
            // NB. it's a post because we don't overide the data, we simply add another line
            _blufs.PostBluf(bluf);
        }

        //GET api/Bluf/GetById/id
        [HttpGet("GetById/{id}")]

        public ActionResult<Bluf> Get(int id)
        {
            var bluf = _blufs.GetById(id);

            if (bluf == null)
            {
                return NotFound();
            }

            return bluf;
        }
    }
}

