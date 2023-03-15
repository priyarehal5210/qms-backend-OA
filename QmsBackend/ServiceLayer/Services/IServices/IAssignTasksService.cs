using DomainLayer.Models;
using ServiceLayer.Dtos;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.IServices
{
    public interface IAssignTasksService
    {
        Task<IEnumerable<AssignTasks>> GetAllAsync(Expression<Func<AssignTasks, bool>> filter = null,
           Func<IQueryable<AssignTasks>, IOrderedQueryable<AssignTasks>> orderby = null,
           string IncludeProperties = null);
        Task AddAsync(AssignTaskDto entity);
        Task<AssignTaskDto> GetByIdAsync(int id);

        Task UpdateAsync(AssignTaskDto entity);
       Task RemoveAsync(int id);
        Task changeStatusAsync(StatusVm statusVm);

    }
}
