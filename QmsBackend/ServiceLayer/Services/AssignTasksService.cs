using AutoMapper;
using DataAccessLayer.Repository;
using DomainLayer.Models;
using ServiceLayer.Dtos;
using ServiceLayer.Services.IServices;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AssignTasksService : IAssignTasksService
    {
        private readonly IRepository<AssignTasks> _assignTasksRepository;
        private readonly IMapper _mapper;
        public AssignTasksService(IRepository<AssignTasks> assignTasksRepository,IMapper mapper)
        {
            _mapper= mapper;
            _assignTasksRepository = assignTasksRepository;
        }

        public async Task AddAsync(AssignTaskDto entity)
        {
            try
            {
                if(entity!= null)
                {
                    var obj=_mapper.Map<AssignTasks>(entity);
                    if(obj!= null)
                    {
                    await _assignTasksRepository.AddAsync(obj);
                    }
              
                }
            }
            catch (Exception) { throw; }
        }

    

        public async Task<IEnumerable<AssignTasks>> GetAllAsync(Expression<Func<AssignTasks, bool>> filter = null, Func<IQueryable<AssignTasks>, IOrderedQueryable<AssignTasks>> orderby = null, string IncludeProperties = null)
        {
            try
            {
                var obj =await _assignTasksRepository.GetAllAsync(IncludeProperties: "register,tasks");
                if(obj!=null)
                {
                    return obj;
                } 
                else return null;
            }catch(Exception) { throw; }
        }


        public async Task RemoveAsync(int id)
        {
            try
            {
                var assigntask = _assignTasksRepository.GetByIdAsync(id);
                await _assignTasksRepository.RemoveAsync(assigntask);
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateAsync(AssignTaskDto entity)
        {
            try
            {

                if (entity!= null) {
                    var obj = _mapper.Map<AssignTasks>(entity);
                    if(obj!= null)
                    {
                       await _assignTasksRepository.UpdateAsync(obj);
                    }
                }
            }
            catch (Exception) { throw; }
        }
        //update status
        public async Task changeStatusAsync(StatusVm statusVm)
        {
            try
            {
                var task = _assignTasksRepository.FirstOrDefault(t => t.registerId == statusVm.register.Id && t.tasksId ==statusVm.tasks.Id);
                if (task.Status != AssignTasks.status.started)
                {
                    task.Status = AssignTasks.status.started;
                    task.isChecked = true;
                    await _assignTasksRepository.SaveChangesAsync();
                }
                else
                {
                    task.Status = AssignTasks.status.completed;
                   await  _assignTasksRepository.SaveChangesAsync();
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<AssignTaskDto> GetByIdAsync(int id)
        {
            try
            {
                var obj = _assignTasksRepository.GetByIdAsync(id);
                var dobj = _mapper.Map<AssignTaskDto>(obj);
                return dobj;

            }
            catch (Exception) { throw; }
        }
    }
}