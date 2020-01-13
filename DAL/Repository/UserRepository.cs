using DAL.Context;
using DAL.Interfaces.Repository;
using DAL.Models.CommonModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepository : IRepository<User>
    {
        private ApplicationContext context;

        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public async void Create(User item)
        {
            if (item !=null)
            {
                await context.Users.AddAsync(item);
            }

           
        }

        public void Delete(int? id)
        {
            var user = Get(id).Result;
            if (user != null)
            {
                context.Users.Remove(user);
            }
            
        }
        public void Delete(User user)
        {
            context.Users.Remove(user);
        }

        public async Task<User> Get(int? id)
        {
           return await context.Users.Include(x => x.Files).ThenInclude(y => y.Link)
                 .Include(x => x.Files).ThenInclude(y => y.Path)
                 .Include(x => x.Files).ThenInclude(y => y.Type)
                 .Include(x => x.Files).ThenInclude(y => y.Status).SingleOrDefaultAsync();
           

        }

        public async Task<User> Get(Expression<Func<User, bool>> filter)
        {
           return await context.Users.Where(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetList()
        {
            return await context.Users
                .Include(x => x.Files).ThenInclude(y => y.Link)
                .Include(x => x.Files).ThenInclude(y => y.Path)
                .Include(x => x.Files).ThenInclude(y => y.Type)
                .Include(x => x.Files).ThenInclude(y => y.Status)
                .ToListAsync();
           
                                                  
        }

        public async Task<IEnumerable<User>> Query(Expression<Func<User, bool>> filter)
        {
           return await context.Users
                .Include(x => x.Files).ThenInclude(y => y.Link)
                .Include(x => x.Files).ThenInclude(y => y.Path)
                .Include(x => x.Files).ThenInclude(y => y.Type)
                .Include(x => x.Files).ThenInclude(y => y.Status)
                .Where(filter)
                .ToListAsync();
           
        }

        public void Update(User item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
