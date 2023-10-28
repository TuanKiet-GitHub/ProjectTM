using QuanLyKhuCongNghiep.Services.Common;
using ThapMuoi.Interfaces.Auth;
using ThapMuoi.Interfaces.Common;
using ThapMuoi.Interfaces.Core;
using ThapMuoi.Services.Auth;
using ThapMuoi.Services.Core;

namespace ThapMuoi.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<DataContext>();

            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IUnitRoleService, UnitRoleService>();


            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IDonViService, DonViService>();


            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IModuleService, ModuleService>();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDiaGioiHanhChinhService, DiaGioiHanhChinhService>();


            services.AddScoped<IFileService, FileService>();
       

            
            services.AddScoped<IFileMinioService, FileMinioService>();
            
            
        }
    }
}
