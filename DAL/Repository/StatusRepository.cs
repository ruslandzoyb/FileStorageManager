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
            
        }

        public  void Delete(int ? id)
        {
            var path = Get(id).Result;
            if (path != null)
            {
                context.Statuses.Remove(path);
            }
            

        }

        public void Delete(Status status)
        {
            context.Remove(status);
        }
       

        public async Task<Status> Get(int? id)
        {
            return await context.Statuses.FindAsync(id);
            
        }

        public async Task<Status> Get(Expression<Func<Status, bool>> filter)
        {
          return await context.Statuses. Where(filter).SingleOrDefaultAsync(); 
        }

        public async Task<IEnumerable<Status>> GetList()
        {
            return await context.Statuses.ToListAsync();
            
        }

        public async Task<IEnumerable<Status>> Query(Expression<Func<Status, bool>> filter)
        {
            return await context.Statuses.Where(filter).ToListAsync();
           
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
