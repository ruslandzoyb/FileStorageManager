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
   public class PathRepository:IRepository<Path>
    {
        private ApplicationContext context;
        public PathRepository(ApplicationContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async void Create(Path item)
        {
            if (item != null)
            {
                await context.Paths.AddAsync(item);
            }
           
        }

        public  void Delete(Path path)
        {
            if (path !=null )
            {
                context.Paths.Remove(path);
            }

                                   

        }

        public void Delete(int? id)
        {
            var path = Get(id).Result;
            if (path!=null)
            {
                Delete(path);
            }
        }

        public async Task<Path> Get(int? id)
        {
            return await context.Paths.FindAsync(id);
           
        }

        public async Task<Path> Get(Expression<Func<Path, bool>> filter)
        {
            return await context.Paths.Where(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Path>> GetList()
        {
            return await context.Paths.Include(x=>x.File).ToListAsync();
            
        }

        public async Task<IEnumerable<Path>> Query(Expression<Func<Path, bool>> filter)
        {
            return await context.Paths.Where(filter).ToListAsync();
            
        }

        public void Update(Path item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
