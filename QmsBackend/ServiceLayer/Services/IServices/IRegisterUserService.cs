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
    public interface IRegisterUserService
    {
        Task<IEnumerable<RegisterUserDto>> GetAllAsync(Expression<Func<RegisterUserDto, bool>> filter = null,
           Func<IQueryable<RegisterUserDto>, IOrderedQueryable<RegisterUserDto>> orderby = null,
           string IncludeProperties = null);
        Task<RegisterUserDto> GetByIdAsync(int id);
        Task AddAsync(RegisterUserDto entity);

        Register Login(string email, string password);
        Task approveuserAsync(ApproveVm approveVm);
    }
}
