using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.AttachmentService
{
    public interface IAttachmentService
    {
       //upload
       public string? Upload(IFormFile File, string FolderName);
       //Delete
       public bool Delete(string FilePath);

    }
}
