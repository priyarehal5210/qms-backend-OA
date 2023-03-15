using DomainLayer.Models;
using ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.IServices
{
    public interface ITaskService
    {
        Task<IEnumerable<TasksDto>> GetAllAsync(Expression<Func<TasksDto, bool>> filter = null,
           Func<IQueryable<TasksDto>, IOrderedQueryable<TasksDto>> orderby = null,
           string IncludeProperties = null);
        Task<IEnumerable<Tasks>> FindAsync(Expression<Func<TasksDto, bool>> predicate);
        Task<TasksDto> GetByIdAsync(int id);
        Task AddAsync(TasksDto entity);
        Task UpdateAsync(TasksDto entity);
        Task RemoveAsync(int id);
    }
}
