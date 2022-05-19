using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFunctionFirst
{
    public interface IContainerService
    {
        Task<List<string>> GetAllContainersAndBlobs();
        Task<List<string>> GetAllContainers();
        Task CreateContainer(string containerName);
        Task DeleteContainer(string containerName);
    }
}
