using AutoMapper;
using BL.Interfaces.OrdersInterfaces;
using BL.Configuration.Mapper;
using BL.ModelsDTO.FileManageDTO;
using BL.ModelsDTO.OtherModels;
using DAL.Interfaces.UnitOfWork;
using DAL.Models.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.IO;
using File = DAL.Models.CommonModels.File;
using System.Xml;
using BL.Configuration;
using BL.Configuration.FileManaging;
using Exeptions.CE;

namespace BL.Services.CommonServices
{
   public class UserService: IUserService
    {
        protected IUnitOfWork database;
         protected IMapper mapper;

    public UserService(IUnitOfWork database)
    {
        this.database = database;
        mapper = new MapperConfiguration(ctg => ctg.AddProfile(new MapperSet())).CreateMapper();
    }
        

    public ChangeStatusView ChangeStatus(ChangeStatusModel model)
    {

        if (model.Status != model.ChangedStatus)
        {
            ChangeStatusView change;
            if (model.ChangedStatus == "Link")
            {
                var file = database.Files.Get(model.Id).Result;
                if (file != null)
                {
                    file.Status = database.Statuses.Get(x => x.Title == "Link").Result;
                        file.Link =new Link()
                        {
                            Code= RandomService.Random()
                    };
                       // file.Link.Code = 
                        change = new ChangeStatusView()
                        {
                            Name = new string(file.Name),
                            Status = new string(file.Status.Title),
                            Link = new string("http://localhost:50761/api/User/Download?link=" + file.Link.Code)
                    };
                    database.Save();
                    return change;
                }
                else
                {
                    throw new Exception();
                }

            }
            else if (model.ChangedStatus == "Closed")
            {
                var file = database.Files.Get(model.Id).Result;
                if (file != null)
                {
                    file.Status = database.Statuses.Get(x => x.Title == "Closed").Result;
                        file.Link.Code = "";
                    change = new ChangeStatusView()
                    {
                        Name = new string(file.Name),
                        Status = new string(file.Status.Title),

                    };
                    database.Save();
                    return change;

                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }

        }
        else
        {
            //todo :Ex!
            throw new Exception();
        }


    }

    public bool Delete(int? id,string user_id)
    {
            var files = database.Files.Query(x => x.User.IdenityId == user_id).Result;
            var file = files.First(x => x.Id == id);
            var path = file.Path.Link;
            database.Files.Delete(file);
             database.Save();
            System.IO.File.Delete(path);

        return true;
        //todo :Delete !!!
    }

    public  FileDownloadModel Download(string link)
    {
            if (string.IsNullOrEmpty(link))
            {
                throw new UserServiceException("Link is null or empty");
            }

            var id = database.Links.Get(x => x.Code == link).Result.Id;
           
                var file = mapper.Map<FileDTO>(database.Files.Get(x => x.Link.Id == id).Result);
            if (file!=null)
            {
                return FileManagment.DownloadFile(file);
            }
            throw new UserServiceException("File is null");
                    
           
            
        }
        public FileDownloadModel Download(int? id)
        {
            var file = mapper.Map<FileDTO>(database.Files.Get(id).Result);
            if (file!=null)
            {
                return FileManagment.DownloadFile(file);
            }
            throw new UserServiceException("File is null");
            
           
        }

        public IEnumerable<FileDTO> Find(Expression<Func<File, bool>> predicate)
    {
        var list = mapper.Map<List<FileDTO>>(database.Files.Query(predicate).Result);
        if (list != null)
        {
            return list;
        }
        else
        {
            throw new Exception();
        }
    }

    

    public FileDTO GetFile(int? id,string user_id)
    {
        var file = mapper.Map<FileDTO>(database.Users.Get(x=>x.IdenityId==user_id).Result.Files.Where(x=>x.Id==id));
        if (file != null)
        {
            return file;
        }
        else
        {
                throw new UserServiceException("File wasn't found or file doesen't belong to user");
        }
    }

    public IEnumerable<FileDTO> GetFiles(string user_id)
    {
        var list = mapper.Map<List<FileDTO>>(database.Files.Query(x => x.User.IdenityId == user_id).Result);
        if (list != null)
        {
            return list;
        }
        else
        {
                
                throw new UserServiceException("Files are null");
        }
    }

    public FileDTO GetPath(string link)
    {
        var l = (database.Links.Get(x => x.Code == link).Result);
            if (l!=null)
            {
                int i = l.Id;
                var el = mapper.Map<FileDTO>(database.Files.Get(x => x.Link.Id == i).Result);
                if (el != null)
                {
                    return el;
                }
                else
                {
                    throw new Exception();
                }
            }
            throw new Exception();
           

    }

    public string InfoByFile(int? id,string user_id)
    {
        var file = mapper.Map<FileDTO>(database.Files.Get(id).Result);
        if (file != null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Name  {file.Name} {Environment.NewLine} Description {file.Description} {Environment.NewLine} Data { file.Creation.ToString() } Environment.NewLine");
            return builder.ToString();
        }
        else
        {
                //todo :ex
                throw new UserServiceException("File wasn't found");
        }
    }



        public bool Upload(FileUploadModel file)
        {

                       
            var status = database.Statuses.Get(x => x.Title == "Closed").Result;
            var user = database.Users.Get(x => x.IdenityId == file.UserId).Result;
            if (user==null)
            {
                throw new UserServiceException("User wasn't found");
            }
            var ex = System.IO.Path.GetExtension(file.File.FileName);

            var type = database.Types.Get(x => x.Format ==ex).Result;
            if (type==null)
            {
                type =new DAL.Models.CommonModels.Type
                {
                    Format = ex
                };

            }


            var path = FileManagment.GeneratePath(file);



            var file_tosave = new File()
            {
                Status = status,
                Description = file.Description,
                User = user,
                Name = file.Name,
                Type = type,
                Path = new DAL.Models.CommonModels.Path()
                {
                    Link = path.Path
                }



            };
            database.Files.Create(file_tosave);
            database.Save();
            return true;
        }

        public IEnumerable<FileDTO> GetList()
        {
          var files=mapper.Map<List<FileDTO>>(database.Files.GetList().Result.ToList());

            if (files!=null)
            {
                return files;
            }
            throw new UserServiceException("Files are null");
            
        }

       

      
}
    
}
