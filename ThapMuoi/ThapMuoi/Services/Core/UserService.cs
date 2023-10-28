using MongoDB.Driver;
using ThapMuoi.Extensions;
using ThapMuoi.Installers;
using ThapMuoi.Interfaces.Core;
using ThapMuoi.Models.Core;

namespace ThapMuoi.Services.Core
{
    public class UserService : BaseService, IUserService
    {
        private DataContext _context;
        private BaseMongoDb<User, string> BaseMongoDb;
        IMongoCollection<User> _collectionUser;
        IMongoCollection<DonVi> _collectionDonVi;
        private IDonViService _donViService;
        public UserService(
            DataContext context,
            IDonViService donViService,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<User, string>(_context.USERS);
            _donViService = donViService;
            _collectionUser = context.USERS;
            _collectionDonVi = context.DONVI;
        }
        


    }
}
