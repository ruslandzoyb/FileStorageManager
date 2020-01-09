using DAL.Interfaces.Repository;
using DAL.Models.CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.UnitOfWork
{
   public interface IUnitOfWork
    {
        IRepository<File> Files { get; }
        IRepository<Link> Links { get; }
        IRepository<Path> Paths { get; }
        IRepository<Status> Statuses { get; }
        IRepository<Models.CommonModels.Type> Types { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
