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
                    file.Link.Code = r.Next(100000, 999999).ToString();
                    change = new ChangeStatusView()
                    {
                        Name = new string(file.Name),
                        Status = new string(file.Status.Title),
                        Link = new string(file.Link.Code)
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
                    file.Link.Code = null;
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

    public bool Delete(int? id,int user_id)
    {
            var files = database.Files.Query(x => x.User.IdenityId == user_id).Result;
            var file = files.First(x => x.Id == id);
        database.Files.Delete(file);
        database.Save();
        return true;
        //todo :Delete !!!
    }

    public FileDownloadModel Download(string link)
    {
            var file = mapper.Map<FileDTO>(database.Links.Get(x => x.Code == link).Result.File);
            if (file != null)
            {
                var download = new FileDownloadModel()
                {
                    Array = System.IO.File.ReadAllBytes(file.Path.Link),
                    Name = file.Name,
                    Type = file.Type.Format
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

    public IEnumerable<FileDTO> GetFiles(int? user_id)
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

    public string InfoByFile(int? id,int user_id)
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



        public string Upload(FileUploadModel file)
        {
            file.UserId = 914;
            var element = file.File;
            var format = element.ContentType;
            var username = file.UserId + " Storage";
            string directory_path= PathConfiguration.storage + @"\" + username;
            if (!Directory.Exists(directory_path))
            {
                directory_path = PathConfiguration.storage + @"\" + username;
                Directory.CreateDirectory(directory_path);
                  }

            string path = directory_path +@"\"+ element.FileName;
            if (element.Length > 0)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    element.CopyTo(fileStream);
                }

            }
            var status = database.Statuses.Get(x => x.Title == "Closed").Result;
            var user = database.Users.Get(x => x.IdenityId == file.UserId).Result;
            DAL.Models.CommonModels.Type type= new DAL.Models.CommonModels.Type();// database.Types.Get(x => x.Format == format).Result;
            if (type==null)
            {
                type = new DAL.Models.CommonModels.Type()
                {
                    Format = format
                };
            }
            var file_tosave = new File()
            {
                Status = status,
                Description = file.Description,
                User = user,
                Name = file.Name,
                Type = type,
                Path = new DAL.Models.CommonModels.Path()
                {
                    Link = path
                }



            };
            database.Files.Create(file_tosave);
            database.Save();
            throw new Exception();
        }

        public IEnumerable<FileDTO> GetList()
        {
          var r=mapper.Map<List<FileDTO>>(database.Files.GetList().Result.ToList());
            return r;
        }

        public static Random r = new Random();
}
}
