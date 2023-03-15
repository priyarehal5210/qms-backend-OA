using AutoMapper;
using DataAccessLayer.Repository;
using DomainLayer.Models;
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
    public class UserSuccessService : IUserSuccessService
    {
        private readonly IRepository<UserSuccess> _userSuccessRepository;
        private readonly IMapper _mapper;
        public UserSuccessService(IRepository<UserSuccess> userSuccessRepository,IMapper mapper)
        {
            _userSuccessRepository = userSuccessRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(UserSuccessDto entity)
        {
            try
            {
                if (entity != null)
                {
                    var obj = _mapper.Map<UserSuccess>(entity);
                    if (obj != null)
                    {
                       await _userSuccessRepository.AddAsync(obj);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserSuccess>> GetAllAsync(Expression<Func<UserSuccess, bool>> filter = null, Func<IQueryable<UserSuccess>, IOrderedQueryable<UserSuccess>> orderby = null, string IncludeProperties = null)
        {
            try
            {
                var obj = await _userSuccessRepository.GetAllAsync(IncludeProperties: "assignTasks,assignTasks.register,assignTasks.tasks");
                var dobj = _mapper.Map<IEnumerable<UserSuccess>>(obj);
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

        public async Task<UserSuccessDto> GetByIdAsync(int id)
        {
            try
            {
                var obj = _userSuccessRepository.GetByIdAsync(id);
                var dobj = _mapper.Map<UserSuccessDto>(obj);
                return dobj;

            }
            catch (Exception) { throw; }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var success = _userSuccessRepository.GetByIdAsync(id);
                await _userSuccessRepository.RemoveAsync(success);
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateAsync(UserSuccessDto entity)
        {
            try
            {
                if (entity != null)
                {
                    var success = _mapper.Map<UserSuccess>(entity);
                    if (success != null)
                    {
                       await _userSuccessRepository.UpdateAsync(success);
                    }
                }
            }
            catch (Exception) { throw; }
        }
    }
}
