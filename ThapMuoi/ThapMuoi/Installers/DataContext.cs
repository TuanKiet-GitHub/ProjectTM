using MongoDB.Driver;
using ThapMuoi.Constants;
using ThapMuoi.Models.Auth;
using ThapMuoi.Models.Core;

namespace ThapMuoi.Installers
{
    public class DataContext
    {
        private readonly IMongoClient _mongoClient = null;
        private readonly IMongoDatabase _context = null;
        private readonly IMongoCollection<CommonModel> _common;
        
        private readonly IMongoCollection<User> _users;

        private readonly IMongoCollection<Module> _module;
        private readonly IMongoCollection<Menu> _menu;
            
        private readonly IMongoCollection<DonVi> _donVi;
        
        
        private readonly IMongoCollection<RefreshTokenModel> _refreshToken;
        private readonly IMongoCollection<UnitRole> _unitRole;
        
        private readonly IMongoCollection<FileModel> _file;
        
        private readonly IMongoCollection<DiaGioiHanhChinhModel> _diaGioiHanhChinh;

        private readonly Dictionary<string,  IMongoCollection<CommonModel>> _listCommonCollection;
        
   

      

        public DataContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.
               GetValue<string>(ConfigurationDb.MONGO_CONNECTION_STRING));
            if (client != null)
            {
                _context = client.GetDatabase(configuration.GetValue<string>(ConfigurationDb.MONGO_DATABASE_NAME));
          
                _users = _context.GetCollection<User>(DefaultNameCollection.USERS);
                _diaGioiHanhChinh = _context.GetCollection<DiaGioiHanhChinhModel>(DefaultNameCollection.DIAGIOIHANHCHINH);
                _refreshToken = _context.GetCollection<RefreshTokenModel>(DefaultNameCollection.REFRESHTOKEN);                
                _menu = _context.GetCollection<Menu>(DefaultNameCollection.MENU);
                _module = _context.GetCollection<Module>(DefaultNameCollection.MODULE);
                _donVi = _context.GetCollection<DonVi>(DefaultNameCollection.DONVI);
                _unitRole =_context.GetCollection<UnitRole>(DefaultNameCollection.UNIT_ROLE);
                _file = _context.GetCollection<FileModel>(DefaultNameCollection.FILES);
                _listCommonCollection = new Dictionary<string,  IMongoCollection<CommonModel>>();
                foreach ( ItemCommon item in ListCommon.listCommon)
                {
                    IMongoCollection<CommonModel> colection = Database.GetCollection<CommonModel>(item.Code);
                    _listCommonCollection.Add(item.Code, colection);
                }
            }
        }

        public IMongoDatabase Database
        {
            get { return _context; }
        }
        public IMongoClient Client
        {
            get { return _mongoClient; }
        }
        
        
        
        public IMongoCollection<RefreshTokenModel> REFRESHTOKEN { get => _refreshToken; }
        
        
        public IMongoCollection<UnitRole> UNIT_ROLE { get => _unitRole; }

        
        public IMongoCollection<DiaGioiHanhChinhModel> DIAGIOIHANHCHINH { get => _diaGioiHanhChinh; }
        
    
        
     

        public IMongoCollection<User> USERS { get => _users; }
        
        public IMongoCollection<DonVi> DONVI { get => _donVi; }
        
        
        
        public IMongoCollection<Module> MODULE { get => _module; }

        public IMongoCollection<Menu> MENU { get => _menu; }
        
        
        public IMongoCollection<FileModel> FILES { get => _file; }

      
        private Dictionary<string,  IMongoCollection<CommonModel>> CommonCollection { get => _listCommonCollection; }
        public  IMongoCollection<CommonModel> GetCategoryCollectionAs(string collectionName)
        {
            return  CommonCollection[collectionName];
        }
        
    }


}