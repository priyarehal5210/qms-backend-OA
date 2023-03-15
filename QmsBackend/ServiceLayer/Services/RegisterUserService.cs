using AutoMapper;
using DataAccessLayer.Repository;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Dtos;
using ServiceLayer.Jwt;
using ServiceLayer.Services.IServices;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IRepository<Register> _registerService;
        private readonly Appsetting _jwt;
        private readonly IMapper _mapper;
        public RegisterUserService(IRepository<Register> registerService, IMapper mapper,IOptions<Appsetting>jwt)
        {
            _jwt = jwt.Value;
            _registerService = registerService;
            _mapper = mapper;
        }

        public async Task AddAsync(RegisterUserDto entity)
        {
            try
            {
                var allusers = await _registerService.GetAllAsync();
                if (entity!=null)
                {
                    var obj=_mapper.Map<Register>(entity);
                    if(obj!=null)
                    {
                        //check
                        if (allusers.Any(u => u.role == "Admin"))
                        {
                            obj.role = "Trainee";
                        }
                        else
                        {
                            obj.role = "Admin";
                            obj.approved = true;
                        }if(allusers.Any(u=>u.userName==obj.userName))
                        {
                            return;
                        }
                        
                       await _registerService.AddAsync(obj);
                    }
                }

            }catch(Exception) { throw; }
        }

  

        public async Task<IEnumerable<RegisterUserDto>> GetAllAsync(Expression<Func<RegisterUserDto, bool>> filter = null, Func<IQueryable<RegisterUserDto>, IOrderedQueryable<RegisterUserDto>> orderby = null, string IncludeProperties = null)
        {
            try
            {
                var users =await _registerService.GetAllAsync();
                var allusers = _mapper.Map<IEnumerable<RegisterUserDto>>(users);
                if (allusers != null)
                {
                    return allusers;
                }
                else
                {
                    return null;
                }
            }catch(Exception) { throw; }
        }

        //login
        public Register Login(string email, string password)
        {
            try
            {
                var UserOfSameEmail=_registerService.FirstOrDefault(u=>u.email==email);
                if(UserOfSameEmail != null && UserOfSameEmail.approved==true) 
                {
                    //here generate token
                    var TokenHandeler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_jwt.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.NameIdentifier, UserOfSameEmail.Id.ToString()),
                    new Claim(ClaimTypes.Role,UserOfSameEmail.role)
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                       , SecurityAlgorithms.HmacSha256Signature)

                    };
                    var token = TokenHandeler.CreateToken(tokenDescriptor);
                    UserOfSameEmail.token = TokenHandeler.WriteToken(token);
                    return UserOfSameEmail;

                }
                return null;
            }
            catch
            (Exception)
            { throw; }
        }

        //approve user 
        public async Task approveuserAsync(ApproveVm approveVm)
        {
            try
            {

             var user= _registerService.FirstOrDefault(u=>u.email== approveVm.email);

                if (user != null)
                {
                    user.approved = true;
                   await _registerService.SaveChangesAsync();
                }
            }catch (Exception) { throw; }
        }

        public async Task<RegisterUserDto> GetByIdAsync(int id)
        {
            try
            {
                var obj = _registerService.GetByIdAsync(id);
                var dobj = _mapper.Map<RegisterUserDto>(obj);
                return dobj;

            }
            catch (Exception) { throw; }
        }
    }
}
