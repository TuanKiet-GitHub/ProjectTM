using ThapMuoi.Models.Core;
using ThapMuoi.ViewModels;

namespace ThapMuoi.Interfaces.Core
{
    public interface IMenuService
    {
        Task<List<MenuTreeVM>> GetTreeList();
        
        Task<dynamic> GetTreeFlatten();
        
        
        Task<dynamic> Create(Menu model);
        Task<dynamic> Update(Menu model);
        Task<dynamic> AddAC(MenuList model);
        Task<dynamic>  DeleteAc(MenuListShort model);
        
        
        Task<List<NavMenuVM>> GetTreeListMenuForCurrentUser(List<Menu> listMenu);


        Task<List<NavMenuVM>> GetTreeListMenuAll(); 
    }
}