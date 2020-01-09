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
            //todo exeption
            //throw new NotImplementedException();
        }

        public async void Delete(int id)
        {
            var path = await context.Paths.FindAsync(id);
            if (path != null)
            {
                context.Paths.Remove(path);
            }
            //todo :ex

        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<Path> Get(int? id)
        {
            var path = await context.Paths.FindAsync(id);
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

        public Task<Path> Get(Expression<Func<Path, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Path>> GetList()
        {
            var list = await context.Paths.ToListAsync();
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

        public async Task<IEnumerable<Path>> Query(Expression<Func<Path, bool>> filter)
        {
            var list = await context.Paths.Where(filter).ToListAsync();
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

        public void Update(Path item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
