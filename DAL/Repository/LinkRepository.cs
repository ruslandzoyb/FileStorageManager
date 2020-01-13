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
   public class LinkRepository:IRepository<Link>
    {
        private ApplicationContext context;

        public LinkRepository(ApplicationContext context)
        {
            this.context = context;
        }
        // TODO : Add else block
        public async void Create(Link item)
        {
            if (item != null)
            {
                await context.Links.AddAsync(item);
            }
        }
        //Todo :add else block
        public void Delete(int? id)
        {
            var link = Get(id).Result;
            if (link != null)
            {
                context.Links.Remove(link);
            }
            else
            {
                //todo :
            }
        }
        public void Delete(Link link)
        {
            context.Links.Remove(link);
        }
        public async Task<Link> Get(int? id)
        {
            return await context.Links.FindAsync(id);
            
          

        }

        public Task<Link> Get(Expression<Func<Link, bool>> filter)
        {
           return context.Links .Include(x=>x.File).Where(filter).FirstAsync();
            
        }

        public async Task<IEnumerable<Link>> GetList()
        {
            return await context.Links.ToListAsync();
            

        }

        public async Task<IEnumerable<Link>> Query(Expression<Func<Link, bool>> filter)
        {
            return await context.Links.Where(filter).ToListAsync();
            
        }

        public void Update(Link item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }

    }
}
