using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Car_Rental.Data.Services
{
    public interface IFileUpload
    {
        Task Upload(IFileListEntry file, string filename);
    }
}
