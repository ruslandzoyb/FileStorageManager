using DAL.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models.CommonModels;
using Type = DAL.Models.CommonModels.Type;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using System.Linq;

namespace DAL.Repository
{
   public class TypeRepository:IRepository<Type>
    {
        private ApplicationContext context;

        public TypeRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public async void Create(Type item)
        {
            if (item != null)
            {
                await context.Types.AddAsync(item);
            }
        }

        public  void Delete(Type type)
        {
           // var type = await context.Types.FindAsync(id);
            context.Types.Remove(type);

        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<Type> Get(int? id)
        {
            return await context.Types.FindAsync(id);
        }

        public async Task<Type> Get(Expression<Func<Type, bool>> filter)
        {
            return await context.Types.Where(filter).FirstAsync();
        }

        public async Task<IEnumerable<Type>> GetList()
        {
            return await context.Types.ToListAsync();
        }

        public async Task<IEnumerable<Type>> Query(Expression<Func<Type, bool>> filter)
        {
            return await context.Types.Where(filter).ToListAsync();
        }

        public void Update(Type item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }
    
}
}
