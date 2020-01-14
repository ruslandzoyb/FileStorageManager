using BL.ModelsDTO.FileManageDTO;
using BL.ModelsDTO.OtherModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BL.Configuration.FileManaging
{
   public class FileManagment
    {
        static public Info GeneratePath(FileUploadModel file)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            else
            {
                var element = file.File;
                
                var username = file.UserId + "Storage";
                string directory_path = PathConfiguration.storage + @"\" + username;
                string path = directory_path + @"\" + element.FileName;
                Info info = new Info() {
                    Path = path,
                     Format=Path.GetExtension(file.File.FileName)
                };
                
                if (!Directory.Exists(directory_path))
                {
                    
                    Directory.CreateDirectory(directory_path);
                    Save(element, path);
                }
                else
                {
                    Save(element, path);
                }
                return info;

                

            }

            
        }

        static public void CreateFolder(string user)
        {
            var path=PathConfiguration.storage+"/"+user;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path+"Storage");
            }
        }

        static public void RemoveFolder(string user)
        {
            string path = PathConfiguration.storage + "/" + user + "Storage";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
        static public FileDownloadModel DownloadFile(FileDTO file)
        {
            if (file != null)
            {
                var download = new FileDownloadModel()
                {
                    Array = System.IO.File.ReadAllBytesAsync((file.Path.Link)).Result,
                    Name = file.Name + file.Type.Format,
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

        private static void Save(IFormFile file,string path)
        {
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

            }
        }
    }
   public struct Info
    {
        public string Path { get; set; }
        public string Format { get; set; }
    }
}
