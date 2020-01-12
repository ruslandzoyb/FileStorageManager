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

namespace BL.Services.CommonServices
{
   public class UserService:IUserService
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
            try
            {
                var id = database.Links.Get(x => x.Code == link).Result.Id;
                var file = mapper.Map<FileDTO>(database.Files.Get(x => x.Link.Id == id).Result);
                if (file != null)
                {
                    var download = new FileDownloadModel()
                    {
                        Array = System.IO.File.ReadAllBytesAsync((file.Path.Link)).Result,
                        Name = file.Name,
                        Type = "application/" + file.Type.Format
                    };
                    //todo :Ex
                    return download != null ? download : throw new Exception();
                }
                else
                {
                    //todo :Ex
                    throw new Exception();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public FileDownloadModel Download(int? id)
        {
            var file = database.Files.Get(id).Result;
            if (file!=null)
            {
               
                var download = new FileDownloadModel()
                {
                    Array = System.IO.File.ReadAllBytesAsync((file.Path.Link)).Result,
                    Name = file.Name,
                    Type = "application/" + file.Type.Format
                };
                //todo :Ex
                return download != null ? download : throw new Exception();
            }
            else
            {
                throw new Exception();
            }
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

    

    public FileDTO GetFile(int? id,int user_id)
    {
        var file = mapper.Map<FileDTO>(database.Users.Get(user_id).Result.Files.Where(x=>x.Id==id));
        if (file != null)
        {
            return file;
        }
        else
        {
            throw new Exception();
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
            //todo :ex
            throw new Exception();
        }
    }

    public FileDTO GetPath(string link)
    {
        var l = (database.Links.Get(x => x.Code == link).Result.Id);
            var el = mapper.Map<FileDTO>( database.Files.Get(x => x.Link.Id == l).Result);
        if (el != null)
        {
            return el;
        }
        else
        {
            throw new Exception();
        }

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
            throw new Exception();
        }
    }



        public bool Upload(FileUploadModel file)
        {

            var path = FileSaver.GeneratePath(file);

           
            var status = database.Statuses.Get(x => x.Title == "Closed").Result;
            var user = database.Users.Get(x => x.IdenityId == file.UserId).Result;
            


            var type = new DAL.Models.CommonModels.Type()
            {
                Format = path.Format
            };

            

            
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
            return files;
        }

       

      
}
    
}
