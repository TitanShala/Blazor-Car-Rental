using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Car_Rental.Data.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _oWebHostEnvironment;

        public FileUpload(IWebHostEnvironment _WebHostEnvironment)
        {
            _oWebHostEnvironment = _WebHostEnvironment;
        }
        public async Task Upload (IFileListEntry file, string filename)
        {
            
            var path = Path.Combine(_oWebHostEnvironment.WebRootPath, "CarImages", filename);
            var memoryStream = new MemoryStream();
            await file.Data.CopyToAsync(memoryStream);

            using(FileStream fileStream = new FileStream(path , FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(fileStream);
            }
        }

        public async Task DeleteOldImage (string imageName)
        {
            var path = Path.Combine(_oWebHostEnvironment.WebRootPath, "CarImages", imageName);
            if (System.IO.File.Exists(path))
            {
                FileInfo fi = new FileInfo(path);
                if (fi != null)
                {
                    System.IO.File.Delete(path);
                    fi.Delete();
                }
            }
        }
    }
}
