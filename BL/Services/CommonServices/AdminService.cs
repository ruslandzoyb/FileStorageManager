using AutoMapper;
using BL.Configuration.Mapper;
using BL.Interfaces.OrdersInterfaces;
using BL.ModelsDTO.ApplicationModels;
using BL.ModelsDTO.FileManageDTO;
using BL.ModelsDTO.OtherModels;
using DAL.Interfaces.UnitOfWork;
using DAL.Models.CommonModels;
using DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.CommonServices
{
    public class AdminService : IAdminService
    {
        private UserManager<ApplicationUser> manager;
        private IUnitOfWork database;
        
        private IMapper mapper;
        public AdminService(IUnitOfWork database, UserManager<ApplicationUser> manager)
        {
            this.database = database;
            this.manager = manager;
            this.mapper = new MapperConfiguration(ctg => ctg.AddProfile(new MapperSet())).CreateMapper();

        }

        public string DeleteUser(string id)
        {
            var iden_user = manager.FindByIdAsync(id).Result;
            if (iden_user != null)
            {
                var name = iden_user.UserName;
                var result = manager.DeleteAsync(iden_user).Result;
                if (result.Succeeded)
                {
                    var user = database.Users.Get(x=>x.IdenityId==id).Result;
                    database.Users.Delete(user);

                    return new string($" User {name} with id {id} was deleted ");
                }
                else
                {
                    throw new Exception();//todo :ex
                }

            }
            else
            {
                throw new Exception(); //todo :ex
            }
        }

        public string FrozeUser(FrozeModel model)
        {
            var user = manager.FindByIdAsync(model.Id).Result;
            if (user != null)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = model.Time;
                var result = manager.UpdateAsync(user).Result;
                if (result.Succeeded)
                {
                    return new string($"User {user.UserName} was frozen in {model.Time} ");
                }
                else
                {
                    throw new Exception();
                }

            }
            else
            {
                throw new Exception();
            }
        }

        public IEnumerable<FileDTO> GetFiles()
        {
            var fk = mapper.Map<Task<FileDTO>>(database.Files.GetList());
            var files = mapper.Map<List<FileDTO>>(database.Files.GetList().Result
                                    .OrderBy(x => x.Name)
                                    .ThenBy(x => x.Creation));

            if (files != null)
            {
                return files;
            }
            //todo :ex
            throw new Exception();
        }

        public Task<List<ApplicationUser>> GetIdentityUsers()
        {
            var users = manager.Users.ToListAsync();
            return users;
        }

        public IEnumerable<StatusDTO> GetStatuses()
        {
            var statuses = mapper.Map<List<StatusDTO>>(database.Statuses.GetList().Result
                                            .OrderBy(x => x.Title)
                                            .ThenBy(x => x.Id));
            if (statuses != null)
            {
                return statuses;
            }
            //todo :Ex
            throw new NotImplementedException();
        }

        public IEnumerable<TypeDTO> GetTypes()
        {
            var categories = mapper.Map<List<TypeDTO>>(database.Types
                          .GetList().Result
                          .OrderBy(x => x.Format)
                          .ThenBy(x => x.Id));
            if (categories != null)
            {
                return categories;
            }
            throw new Exception();//todo:ex


        }

        public UserDTO GetUser(string id)
        {
            var user = mapper.Map<UserDTO>(database.Users.Get(x=>x.IdenityId==id).Result);
            if (user != null)
            {
                return user;

            }
            else
            {
                throw new Exception();//todo :ex
            }
        }

        public ApplicationUserDTO GetUserInfo(string id)
        {
            var user = mapper.Map<ApplicationUserDTO>(manager.FindByIdAsync(id).Result);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception(); //todo :ex

            }
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var users = mapper.Map<List<UserDTO>>(database.Users.GetList().Result);
            if (users != null)
            {
                return users.ToList();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
