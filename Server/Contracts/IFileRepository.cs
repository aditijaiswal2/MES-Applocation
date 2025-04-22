using iTextSharp.text.rtf.graphic;
using MES.Shared.Models;
using MES.Shared.Models.Rotors;

namespace MES.Server.Contracts
{
    public interface IFileRepository : IRepositoryBase<SalesAttachedFile>
    {
      
        Task<SalesAttachedFile> AddFileAsync(SalesAttachedFile salesAttachedFile);
        Task<SalesAttachedFile> GetfilesByPartNumberAsync(string id);


    }
}
