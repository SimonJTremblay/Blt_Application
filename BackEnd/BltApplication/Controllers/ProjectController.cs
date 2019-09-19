using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BltApplication.Models;
using BltData;
using BltData.Models;

namespace BltApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        //private readonly BltContext _context;

        //public ProjectController(BltContext context)
        //{
        //    _context = context;
        //}
        private readonly IProject _projects;
        public ProjectController(IProject projects)
        {
            _projects = projects;
        }

        //GET api/Project
        [HttpGet]
        public ActionResult<IEnumerable<Project>> Get()
        {
            var projectList = _projects.GetAll().ToList();
            return projectList;
        }

        //GET api/Project/GetById/id
        [HttpGet("GetById/{id}")]

        public ActionResult<Project> Get(int id)
        {
            var project = _projects.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // GET api/Project/GetDeliverablesFromProject/id
        [HttpGet("GetDeliverablesFromProject/{projectId}")]
        public ActionResult<IEnumerable<Deliverable>> GetDeliverablesFromProject(int projectId)
        {
            var deliverableList = _projects.GetDeliverablesFromProject(projectId).ToList();

            if (deliverableList == null)
            {
                return NotFound();
            }

            return deliverableList;
        }

        //// GET: api/Project
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Project>>> Get()
        //{
        //    return await _context.Project.ToListAsync();
        //}

        //// GET: api/Project/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Project>> Get(int id)
        //{
        //    var project = await _context.Project.FindAsync(id);

        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    return project;
        //}

        //// PUT: api/Project/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProject(int id, Project project)
        //{
        //    if (id != project.ProjectId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(project).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProjectExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Project
        //[HttpPost]
        //public async Task<ActionResult<Project>> PostProject(Project project)
        //{
        //    _context.Project.Add(project);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProject", new { id = project.ProjectId }, project);
        //}

        //// DELETE: api/Project/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Project>> DeleteProject(int id)
        //{
        //    var project = await _context.Project.FindAsync(id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Project.Remove(project);
        //    await _context.SaveChangesAsync();

        //    return project;
        //}

        //private bool ProjectExists(int id)
        //{
        //    return _context.Project.Any(e => e.ProjectId == id);
        //}
    }
}
