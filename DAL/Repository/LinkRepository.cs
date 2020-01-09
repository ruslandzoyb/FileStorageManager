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
            var link = await context.Links.FindAsync(id);
            if (link != null)
            {
                return link;
            }
            else
            {
                //todo : make user exeption
                throw new Exception();
            }

        }

        public Task<Link> Get(Expression<Func<Link, bool>> filter)
        {
            var link = context.Links.Where(filter).FirstAsync();
            if (link != null)
            {
                return link;
            }
            else
            {
                //todo
                throw new Exception();
            }
        }

        public async Task<IEnumerable<Link>> GetList()
        {
            var list = await context.Links.ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                //todo exeption 
                throw new Exception();
            }

        }

        public async Task<IEnumerable<Link>> Query(Expression<Func<Link, bool>> filter)
        {
            var list = await context.Links.Where(filter).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                //todo exeption
                throw new Exception();
            }
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
