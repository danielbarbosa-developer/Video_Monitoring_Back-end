using System.Buffers.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Abstractions.ApplicationAbstractions
{
    public interface IVideoService<TModel> where TModel : IDto
    {
        Task<string> DownloadVideo(string id);
        Task<TModel> GetVideoInformation(string id);
        Task<IEnumerable<TModel>> GetAllVideosInformation(string serverId);
    }
}