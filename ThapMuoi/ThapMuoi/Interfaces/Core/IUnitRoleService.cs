using ThapMuoi.FromBodyModels;
using ThapMuoi.Models.Core;

namespace ThapMuoi.Interfaces.Core
{
    public interface IUnitRoleService
    {
        Task<dynamic> Create(UnitRole model);
        Task<dynamic> Update(UnitRole model);

        Task<dynamic> GetByCurrentUser();
        
        Task<dynamic> UpdateAction(IdFromBodyUnitRole model);
        




    }
}