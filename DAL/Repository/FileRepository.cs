﻿using DAL.Context;
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
  public  class FileRepository:IRepository<File>
    {
        private ApplicationContext context;
        public FileRepository(ApplicationContext context)
        {
            this.context = context;

        }
        public async void Create(File item)
        {
            if (item != null)
            {
                item.Creation = DateTime.Now;
                await context.Files.AddAsync(item);
            }

        }

        public void Delete(File item)
        {
            if (item!=null)
            {
                context.Files.Remove(item);
            }
            
        }
        public void Delete(int? id)
        {
            var file =  Get(id).Result;
            if (file != null)
            {
                context.Files.Remove(file);
            }
           
        }


        public async Task<File> Get(int? id)
        {
            File file = await context.Files.Include(x=>x.Link).Include(x => x.Link)
                 .Include(x => x.Path)
                 .Include(x => x.Status)
                 .Include(x => x.Type)
                 .Include(x => x.User)
                 
                .Where(x => x.Id == id).SingleOrDefaultAsync();

            

            return file;

        }

        public async Task<File> Get(Expression<Func<File, bool>> filter)
        {
            return await context.Files.Include(x => x.Path)
                .Include(x => x.Status)
                .Include(x => x.Type)
                .Include(x=>x.Link)
                .Include(x=>x.Type)
                .Where(filter)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<File>> GetList()
        {
            return await context.Files.Include(x => x.Link)
                 .Include(x => x.Path)
                .Include(x => x.Status)
                .Include(x => x.Type)
                .Include(x => x.User)
                 .ToListAsync();
            //

        }

        public async Task<IEnumerable<File>> Query(Expression<Func<File, bool>> filter)
        {
            return await context.Files.Include(x => x.Path)
                 .Include(x => x.Status)
                 .Include(x => x.Type)
                 .Where(filter)
                 .ToListAsync();




        }

        public void Update(File item)
        {
            if (item != null)
            {
                context.Entry(item).State = EntityState.Modified;
            }

        }
    

}
}
