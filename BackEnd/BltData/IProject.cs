using BltApplication.Models;
using BltData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BltData
{
    public interface IProject
    {
        IEnumerable<Project> GetAll();
        Project GetById(int id);
        void Add(Project project);
        void Delete(Project project);
        IEnumerable<Deliverable> GetDeliverablesFromProject(int projectId);
        IEnumerable<Deliverable> GetDeliverablesAndBlufFromProject(int projectId);

    }
}
