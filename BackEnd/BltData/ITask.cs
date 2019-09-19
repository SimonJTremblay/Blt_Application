using BltData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BltData
{
    public interface ITask
    {
        IEnumerable<Task> GetAll();
        IEnumerable<Task> GetTasksFromDeliverable(int deliverableId);
        Task GetById(int id);
        void Add(Task task);
        void Delete(Task task);
    }
}
