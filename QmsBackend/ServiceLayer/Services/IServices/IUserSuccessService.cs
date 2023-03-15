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
    public interface IUserSuccessService
    {
        Task<IEnumerable<UserSuccess>> GetAllAsync(Expression<Func<UserSuccess, bool>> filter = null,
      Func<IQueryable<UserSuccess>, IOrderedQueryable<UserSuccess>> orderby = null,
      string IncludeProperties = null);
        Task AddAsync(UserSuccessDto entity);
        Task<UserSuccessDto> GetByIdAsync(int id);
        Task UpdateAsync(UserSuccessDto entity);
        Task RemoveAsync(int id);
    }
}
