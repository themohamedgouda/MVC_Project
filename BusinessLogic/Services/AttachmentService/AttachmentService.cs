using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLogic.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> AllowedExtensions = [".png", ".jpg",".jpeg" ];
        const int  MaxSize = 2_097_152;

        public bool Delete(string FilePath)
        {
            if(!File.Exists(FilePath)) return false;
            File.Delete(FilePath);
            return true;
        }

        public string? Upload(IFormFile File, string FolderName)
        {
            var Extension = Path.GetExtension(File.FileName);
            //1- Checked Ext
            if (!AllowedExtensions.Contains(Extension)) return null;
            //Checked NULL
            if (FolderName == null) return null;
            //2- Checked Size
            if (File.Length == 0 || File.Length > MaxSize) return null;
            //3- Checked Located Folder Path
            // var FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{FolderName}";
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", FolderName);
            //4- Make Name UNIQUE
            var FileName = $"{Guid.NewGuid()}_{File.FileName}";
            //5- Get File Path
            var FilePath = Path.Combine(FolderPath,FileName);
            //6- File Stream to copy
            using FileStream FileStream = new FileStream(FilePath, FileMode.Create); 
            //7- Use Stream
            File.CopyTo(FileStream);
            //8-  Return File Name
            return FileName;

        }
    }
}
