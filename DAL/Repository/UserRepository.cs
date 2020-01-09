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
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            await context.Users.AddAsync(item);
        }

        public void Delete(int? id)
        {
            var user = Get(id).Result;
            if (user != null)
            {
                context.Users.Remove(user);
            }
            else
            {
                //todo :
            }
        }

        public async Task<User> Get(int? id)
        {
            var user = await context.Users.Include(x => x.Files).ThenInclude(y => y.Link)
                 .Include(x => x.Files).ThenInclude(y => y.Path)
                 .Include(x => x.Files).ThenInclude(y => y.Type)
                 .Include(x => x.Files).ThenInclude(y => y.Status).FirstAsync();
            if (user != null)
            {
                return user;
            }
            else
            {
                //todo :ex
                throw new Exception();
            }

        }

        public Task<User> Get(Expression<Func<User, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetList()
        {
            var users = await context.Users
                .Include(x => x.Files).ThenInclude(y => y.Link)
                .Include(x => x.Files).ThenInclude(y => y.Path)
                .Include(x => x.Files).ThenInclude(y => y.Type)
                .Include(x => x.Files).ThenInclude(y => y.Status)
                .ToListAsync();
            if (users != null)
            {
                return users;
            }
            else
            {
                //todo:ex
                throw new Exception();
            }






        }

        public async Task<IEnumerable<User>> Query(Expression<Func<User, bool>> filter)
        {
            var users = await context.Users
                .Include(x => x.Files).ThenInclude(y => y.Link)
                .Include(x => x.Files).ThenInclude(y => y.Path)
                .Include(x => x.Files).ThenInclude(y => y.Type)
                .Include(x => x.Files).ThenInclude(y => y.Status)
                .Where(filter)
                .ToListAsync();
            if (users != null)
            {
                return users;
            }
            else
            {
                //todo :ex
                throw new Exception();
            }
        }

        public void Update(User item)
        {
            //Todo :Update !!!!!
        }
    }
}
