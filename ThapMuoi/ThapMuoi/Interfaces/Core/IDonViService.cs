using ThapMuoi.Models.Core;

namespace ThapMuoi.Interfaces.Core
{
    public interface IDonViService
    {
        Task<dynamic> Create(DonVi model);
        Task<dynamic> Update(DonVi model);
        Task<dynamic> GetTree();
        
        
        List<string> GetListDonViId(string donViId);
        
        Task<dynamic> GetTreeAll();
        
        
        
        Task<dynamic> GetTreeFlatten();
        

        
        
        Task<dynamic> GetTreeSelected();
        
    }
}