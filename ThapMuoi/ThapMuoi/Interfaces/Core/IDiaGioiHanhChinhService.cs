using ThapMuoi.Models.Core;

namespace ThapMuoi.Interfaces.Core
{
    public interface IDiaGioiHanhChinhService
    {
        Task<dynamic> Create(DiaGioiHanhChinhModel model);
        Task<dynamic> Update(DiaGioiHanhChinhModel model);
        
        Task<dynamic> GetListChildByCode(string code);
        
        
        
        Task<dynamic> GetListByLevel(int level);
        
        
        
        
    }
}