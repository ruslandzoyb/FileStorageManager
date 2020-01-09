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

namespace BL.Services.CommonServices
{
   public class UserService:IUserService,ILinkService
    {
        protected IUnitOfWork database;
         protected IMapper mapper;

    protected UserService(IUnitOfWork database)
    {
        this.database = database;
        mapper = new MapperConfiguration(ctg => ctg.AddProfile(new MapperSet())).CreateMapper();
    }
        public UserService()
        {

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

    public bool Delete(int? id)
    {
        database.Files.Delete(id);
        return true;
        //todo :Delete !!!
    }

    public FileDTO Download(int? id)
    {
        var file = mapper.Map<FileDTO>(database.Files.Get(id));
        if (file != null)
        {
            return file;
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

    //public FileDTO GetCommon(int? id)
    //{
    //    throw new NotImplementedException();
    //}

    public FileDTO GetFile(int? id)
    {
        var file = mapper.Map<FileDTO>(database.Files.Get(id));
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
        var l = mapper.Map<FileDTO>(database.Links.Get(x => x.Code == link).Result.File);

        if (l != null)
        {
            return l;
        }
        else
        {
            throw new Exception();
        }

    }

    public string InfoByFile(int? id)
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



    public bool Upload(FileDownloadModel file)
    {


        return true;
    }


    public static Random r = new Random();
}
}
