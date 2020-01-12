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
  public  class StatusRepository:IRepository<Status>
    {
        private ApplicationContext context;
        public StatusRepository(ApplicationContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async void Create(Status item)
        {
            if (item != null)
            {
                await context.Statuses.AddAsync(item);
            }
            //todo exeption
            throw new NotImplementedException();
        }

        public async void Delete(int ? id)
        {
            var path = await context.Statuses.FindAsync(id);
            if (path != null)
            {
                context.Statuses.Remove(path);
            }
            //todo :ex

        }

        public void Delete(Status status)
        {
            context.Remove(status);
        }
       

        public async Task<Status> Get(int? id)
        {
            var path = await context.Statuses.FindAsync(id);
            if (path != null)
            {
                return path;
            }
            else
            {
                //todo :ex
                throw new Exception();
            }
        }

        public async Task<Status> Get(Expression<Func<Status, bool>> filter)
        {
          return await context.Statuses. Where(filter).FirstAsync(); 
        }

        public async Task<IEnumerable<Status>> GetList()
        {
            var list = await context.Statuses.ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                //todo :ex
                throw new Exception();
            }
        }

        public async Task<IEnumerable<Status>> Query(Expression<Func<Status, bool>> filter)
        {
            var list = await context.Statuses.Where(filter).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                //todo:ex
                throw new Exception();
            }
        }

        public void Update(Status item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }


        }
    }
}
