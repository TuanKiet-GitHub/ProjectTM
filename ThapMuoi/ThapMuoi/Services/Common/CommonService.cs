using ThapMuoi.Installers;
using ThapMuoi.Interfaces.Common;
using ThapMuoi.Models.Core;
using ThapMuoi.Services.Core;

namespace QuanLyKhuCongNghiep.Services.Common;

public class CommonService: CommmonRepository<CommonModel, string>, ICommonService
{
       
    public CommonService(DataContext context, IHttpContextAccessor contextAccessor) :
        base(context, contextAccessor)
    {
    }
    
        
}