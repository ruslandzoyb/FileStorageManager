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
using System.Threading.Tasks;
using System.Linq;
using BL.Configuration.FileManaging;
using Exeptions.CE;
using BL.Configuration;

namespace BL.Services.CommonServices
{
   public class AccountService : IAccountService
    {
        IUnitOfWork database;
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> rolesManger;
        SignInManager<ApplicationUser> signManager;
         IMapper mapper;
        public AccountService(IUnitOfWork database,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> rolesManger, SignInManager<ApplicationUser> signManager)
        {
            this.userManager = userManager;
            this.database = database;
            this.rolesManger = rolesManger;
            this.signManager = signManager;
            mapper= new MapperConfiguration(ctg => ctg.AddProfile(new MapperSet())).CreateMapper();

            

        }
       

        public bool CreateUser(ApplicationUserDTO user)
        {
            if (user is null)
            {
                throw new AccountException("User is null");
            }

            var us = new ApplicationUser()
            {
                Email = user.Email,
                Surname = user.Surname,
                UserName=user.Name
                
                                


            };
            var role = user.Roles;
           

            var create = userManager.CreateAsync(us, user.Password).Result;
            var roles = userManager.AddToRolesAsync(us, role).Result;
           
                
                if (create.Succeeded && roles.Succeeded)
                {
                    
                    database.Users.Create(mapper.Map<User>(new UserDTO()
                    {
                        IdenityId = userManager.GetUserIdAsync(us).Result
                        
                    }));
                    string path = userManager.GetUserIdAsync(us).Result;
                    FileManagment.CreateFolder(path);




                    database.Save();
                    return true;

                }
                else
                {
                throw new AccountException("User or Role wasn't created");
                
            }
          
                       
        }

        public string DeleteAccount(DeleteAccModel model)
        {
            if (model != null)
            {
                var user = userManager.FindByIdAsync(model.Id).Result;
                
                if (user!=null&&userManager.CheckPasswordAsync(user,model.Password).Result)
                {
                    var name = user.UserName;
                    var surname = user.Surname;
                    var delete = userManager.DeleteAsync(user).Result;
                    
                    if (delete.Succeeded)
                    {
                        var obj = database.Users.Get(x=>x.IdenityId==model.Id).Result;
                        database.Users.Delete(obj);
                        database.Save();
                        FileManagment.RemoveFolder(model.Id);
                        return new string($"User {name} {surname} account was deleted cause of {model.Reason} ");
                    }
                    else
                    {
                        throw new AccountException("Removal failed");
                    }
                }
                else
                {
                    throw new AccountException("Password was not confirmed");
                }
               
            }
            else
            {
                throw new AccountException("Delete model is null");
            }
        }

        public   string Login(LoginModelDTO model)
        {
            if (model is null)
            {
                throw new AccountException("Login model is null");
            }


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
                    throw new AccountException("Claims are null");
                }
                
            }
            else
            {
                throw new AccountException("Password was not confirmed");
            }

           



        }

        
    }
}
