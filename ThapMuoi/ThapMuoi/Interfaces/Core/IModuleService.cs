using ThapMuoi.FromBodyModels;
using ThapMuoi.Models.PagingParam;

namespace ThapMuoi.Interfaces.Core
{
    public interface IModuleService
    {
        Task<dynamic> GetAllCore();
        Task<dynamic> GetById(IdFromBodyModel fromBodyModel);
        Task<dynamic> GetPagingCore(PagingParam pagingParam);
    }
}