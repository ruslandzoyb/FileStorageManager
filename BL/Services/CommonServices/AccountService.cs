using AutoMapper;
using BL.Configuration.TokenServices;
using BL.Interfaces.OrdersInterfaces;
using BL.Configuration.Mapper;
using BL.ModelsDTO.ApplicationModels;
using BL.ModelsDTO.FileManageDTO;
using DAL.Context;
using DAL.Interfaces.UnitOfWork;
using DAL.Models.CommonModels;
using DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


using System;
using System.Collections.Generic;
using System.Text;
using BL.ModelsDTO.OtherModels;

namespace BL.Services.CommonServices
{
   public class AccountService : IAccountService
    {
        IUnitOfWork database;
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> rolesManger;
        private IMapper mapper;
        public AccountService(IUnitOfWork database,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> rolesManger)
        {
            this.userManager = userManager;
            this.database = database;
            this.rolesManger = rolesManger;
            mapper= new MapperConfiguration(ctg => ctg.AddProfile(new MapperSet())).CreateMapper();

            //todo :inject !!!!!!!!

        }
        public void CreateAdmin(string model)
        {
            
        }

        public bool CreateUser(ApplicationUserDTO user)
        {
            var us = new ApplicationUser()
            {
                Email = user.Email,
                Surname = user.Surname,
                UserName=user.Name
                                


            };
            var role = rolesManger.CreateAsync(new IdentityRole()
            {
                Name = "User"

            }).Result;

            var create = userManager.CreateAsync(us, user.Password).Result;
            if (create.Succeeded)
            {
                var roles = userManager.AddToRolesAsync(us, user.Roles).Result;
                if (create.Succeeded && roles.Succeeded)
                {

                    database.Users.Create(mapper.Map<User>(new UserDTO()
                    {
                        IdenityId = userManager.GetUserIdAsync(us).Result
                    }));
                    database.Save();
                    return true;

                }
                else
                {
                    return false;
                }
            }
          
            
            
            else
            {
                return false;
            }
            
          
            

           
           
        }

        public string DeleteAccount(DeleteAccModel model)
        {
            if (model != null)
            {
                var user = userManager.FindByIdAsync(model.Id.ToString()).Result;
                if (user!=null&&userManager.CheckPasswordAsync(user,model.Password).Result)
                {
                    var name = user.UserName;
                    var surname = user.Surname;
                    var delete = userManager.DeleteAsync(user).Result;
                    if (delete.Succeeded)
                    {
                        database.Users.Delete(model.Id);
                        database.Save();
                        return new string($"User {name} {surname} account was deleted cause of {model.Reason} ");
                    }
                    else
                    {
                        throw new Exception();///todo :ex
                    }
                }
                else
                {
                    throw new Exception();//todo: ex
                }
               
            }
            else
            {
                throw new ArgumentNullException(nameof(model));//todo :ex
            }
        }

        public   string Login(LoginModelDTO model)
        {
           
            var user =  userManager.FindByEmailAsync(model.Email).Result;
            

            if (user != null && userManager.CheckPasswordAsync(user, model.Password).Result)
            {
                var roles= userManager.GetRolesAsync(user).Result;
                var claims = new ClaimsService().GetClaims(user, roles);
                if (claims!=null)
                {
                    var token = JWT_Service.GetToken(claims);
                    return token;
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
    }
}
