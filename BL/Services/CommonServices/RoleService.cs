using AutoMapper;
using BL.Configuration.Mapper;
using BL.Interfaces.OrdersInterfaces;
using BL.ModelsDTO.ApplicationModels;
using BL.ModelsDTO.OtherModels;
using DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Services.CommonServices
{
    public class RoleService : IRoleService
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private IMapper mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            mapper = new MapperConfiguration(ctg => ctg.AddProfile(new MapperSet())).CreateMapper();
        }

        public bool AddRole(string role)
        {
            
            if (role!=null)
            {
               var result= roleManager.CreateAsync(new IdentityRole()
                {
                    Name = role
                }).Result;
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //todo:Ex
                throw new Exception();
            }
        }

        public string Delete(string role)
        {
           var r = roleManager.FindByNameAsync(role).Result;
            if (r!=null)
            {
               var result= roleManager.DeleteAsync(r).Result;
                if (result.Succeeded)
                {
                    return new string($"Role {role} was deleted ");
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

        public IEnumerable<RoleModel> GetRoles()
        {
            var roles = roleManager.Roles.ToListAsync().Result;
            var model=new List<RoleModel>();
            if (roles!=null)
            {
                foreach (var role in roles)
                {
                    
                    model.Add(new RoleModel()
                    {
                        Id = role.Id,
                        Role = role.Name
                    });
                }
                return model;
            }
            else
            {
                //todo :Ex
                throw new Exception();
            }
           

        }

        public IEnumerable<ApplicationUserDTO> GetUsersByRole(string role)
        {
            if (role is null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            else
            {
                var users =  mapper.Map<List<ApplicationUserDTO>>(userManager.GetUsersInRoleAsync(role).Result.ToList());
                if (users!=null)
                {
                    return users;
                }
                else
                {
                    //todo :Ex
                    throw new Exception();
                }
            }
        }
    }
}
