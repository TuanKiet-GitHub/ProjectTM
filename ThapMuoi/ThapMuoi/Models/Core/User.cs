using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ThapMuoi.ViewModels;

namespace ThapMuoi.Models.Core
{
     public class User : Audit, TEntity<string>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FullName { get; set; }
        
        public string Avatar { get; set; }
        
        public DonViShort DonVi { get; set; }

        
        public List<string> DonViIds { get; set; } = new List<string>();

        public List<string> DonViIdsByUnitRole { get; set; } = new List<string>(); // Dựa theo Id đơn vị ngoại lệ của UnitRole



        public List<DonViShort> UnitTreeRange { get; set; } = new List<DonViShort>(); // Đơn vị này dùng để phân quyền cây đơn vị


        [BsonIgnore]
        public UnitRoleShort UnitRoleView { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public string UnitRoleId { get; set; }

        public string UnitRoleCode { get; set; }


        public int LevelUnitRole { get; set; }


        public bool IsAppAuthentication { get; set; } = false;
        public bool IsVerified { get; set; }
        public bool IsActived { get; set; } = true;
        public bool IsSyncPasswordSuccess { get; set; } = true;
        [BsonIgnore]   public bool IsRequireChangePassword { get; set; } = false;
        [BsonIgnore] public string Password { get; set; }
        
        [BsonIgnore] public string OldPassword { get; set; }

        [BsonIgnore] public List<dynamic> ability { get; set; } = new List<dynamic>();


        [BsonIgnore] public List<string> Permissions { get; set; }
        [BsonIgnore] public List<NavMenuVM> Menu { get; set; }

        [BsonIgnore] public string Token { get; set; }
        

        public bool IsOnlySeeMe { get; set; } = true; // Chỉ xem mình thông tin của tôi  ngược lại  bằng true xem được hết tất cả user của đơn vị đóa và đơn vị con.
    }
}