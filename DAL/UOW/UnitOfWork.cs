using DAL.Context;
using DAL.Interfaces.Repository;
using DAL.Interfaces.UnitOfWork;
using DAL.Models.CommonModels;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.UOW
{
   public class UnitOfWork:IUnitOfWork
    {
        private FileRepository fileRepository;
        private LinkRepository linkRepository;
        private TypeRepository typeRepository;
        private StatusRepository statusRepository;
        private PathRepository pathRepository;
        private UserRepository userRepository;
        //Todo:Repository!!!!!!!
       // private UserManagerRepository userManagerRepository;



        private ApplicationContext context;
        public UnitOfWork(ApplicationContext context)
        {
            this.context = context;
        }

        public IRepository<File> Files
        {
            get
            {
                if (fileRepository == null)
                {
                    fileRepository = new FileRepository(context);

                }
                return fileRepository;

            }

        }

        public IRepository<Link> Links
        {
            get
            {
                if (linkRepository == null)
                {
                    linkRepository = new LinkRepository(context);
                }
                return linkRepository;
            }

        }

        public IRepository<Path> Paths
        {
            get
            {
                if (pathRepository == null)
                {
                    pathRepository = new PathRepository(context);

                }
                return pathRepository;
            }
        }

        public IRepository<Status> Statuses
        {
            get
            {
                if (statusRepository == null)
                {
                    statusRepository = new StatusRepository(context);
                }
                return statusRepository;
            }
        }

        public IRepository<Models.CommonModels.Type> Types
        {
            get
            {
                if (typeRepository == null)
                {
                    typeRepository = new TypeRepository(context);
                }
                return typeRepository;
            }
        }

        //public UserManagerRepository UserManager
        //{
        //    get
        //    {
        //        if (userManagerRepository == null)
        //        {

        //        }
        //        return userManagerRepository;
        //    }
        //}

        public IRepository<User> Users
        {
            get
            {

                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();

        }
    }
}
