using AutoMapper;
using DataAccessLayer.Repository;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ServiceLayer.Dtos;
using ServiceLayer.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class TasksService : ITaskService
    {
        private readonly IRepository<Tasks> _taskRepository;
        private readonly IMapper _mapper;
        public TasksService(IRepository<Tasks> taskRepository,IMapper mapper)
        {
            _mapper= mapper;
            _taskRepository = taskRepository;
        }

        public async Task AddAsync(TasksDto entity)
        {
            try
            {
                if (entity != null)
                {
                    var obj = _mapper.Map<Tasks> (entity);
                    if (obj != null)
                    {
                       await _taskRepository.AddAsync(obj);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<Tasks>> FindAsync(Expression<Func<TasksDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TasksDto>> GetAllAsync(Expression<Func<TasksDto, bool>> filter = null, Func<IQueryable<TasksDto>, IOrderedQueryable<TasksDto>> orderby = null, string IncludeProperties = null)
        {
            try
            {
                var obj = await _taskRepository.GetAllAsync();
                var dobj=_mapper.Map<IEnumerable<TasksDto>>(obj);
                if (dobj != null)
                {
                    return dobj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<TasksDto> GetByIdAsync(int id)
        {
            try
            {
                var obj = _taskRepository.GetByIdAsync(id);
                var dobj = _mapper.Map<TasksDto>(obj);
                return dobj;

            }
            catch (Exception) { throw; }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var task = _taskRepository.GetByIdAsync(id);
                await _taskRepository.RemoveAsync(task);
                
            }
            catch(Exception) { throw; }
        }

        public async Task UpdateAsync(TasksDto entity)
        {
            try
            {
                if (entity != null)
                {
                    var task = _mapper.Map<Tasks>(entity);
                    if(task != null)
                    {
                       await _taskRepository.UpdateAsync(task);
                    }
                }
            }
            catch (Exception) { throw; }
        }

    }
}
