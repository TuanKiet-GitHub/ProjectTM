using ThapMuoi.Interfaces.Core;

namespace ThapMuoi.Models.Core
{
    public class UnitRole : Audit , TEntity<string>
    {
        public string Code { get; set; }
        
        public int Sort { get; set; }
        
        public bool IsSpecialUnit { get; set; } = false; // Nếu bằng true thì cho hiển thị don vi va donviId 

        public List<DonViShort> DonVis { get; set; } = new List<DonViShort>(); // Đơn vị ngoài đơn vị con 

        public List<string> DonViIds { get; set; } = new List<string>(); // Danh Sach ID don vi 

        public List<DonViShort> UnitUsing { get; set; } = new List<DonViShort>(); // Đơn vị sử dụng quyền được tạo


        public List<string> UnitUsingIds { get; set; } = new List<string>();  // Đơn vị sử dụng quyền được tạo string
        

        public bool IsOnlySeeMe { get; set; } = true; // Chỉ xem mình thông tin của tôi  ngược lại  bằng false xem được hết tất cả thông tin của  đơn vị đóa và đơn vị con. 

        public List<string> ListAction { get; set; } = new List<string>(); // Danh sách action của quyền  
        public List<string> ListMenu { get; set; } = new List<string>(); // Danh sách Menu mà quyền này có 
        
        public int Level { get; set; }  // Cho phép quyền này thấy những quyền ngang cấp và dưới cấp


        public void SetUnitRole(IDonViService service)
        {
            DonViIds = new List<string>();
            UnitUsingIds = new List<string>();
            if (IsSpecialUnit && DonVis != default && DonVis.Count > 0)
            {
                foreach (var item in DonVis)
                {
                    DonViIds.AddRange(service.GetListDonViId(item.Id));
                }
            }
            if (UnitUsing != default && UnitUsing.Count > 0)
            {
                foreach (var item in UnitUsing)
                {
                    UnitUsingIds.Add(item.Id);
                }
            }
        }
        
        
        public void SetAction(List<string> listAction)
        {
            ListMenu.Clear();
            ListAction.Clear();
            foreach (var item in  listAction)
            {
                if(item == null)
                    continue;
                var action = item.Split("-");
                if(action[1].Equals(""))
                    continue;
                var key = ListMenu.Where(x => x == action[1]).FirstOrDefault();
                if (key == null)
                {
                    ListMenu.Add(action[1]);
                }
            }
            ListAction = listAction;
        }
    }

    public class UnitRoleShort 
    {
        public  string Id { get; set; }
        
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsSpecialUnit { get; set; }

        public bool IsOnlySeeMe { get; set; } = true;
        
        public int Level { get; set; }
     
        public List<string> ListMenu { get; set; } = new List<string>(); // Danh sách action của menu

        public List<string> DonViIds { get; set; } = new List<string>();  // Danh Sach ID don vi 

        public List<DonViShort> DonVis { get; set; } = new List<DonViShort>();// Danh Sach ID don vi 

        public List<DonViShort> UnitUsing { get; set; } = new List<DonViShort>();// Đơn vị sử dụng quyền được tạo
        
        public List<string> ListAction { get; set; } = new List<string>(); // Danh sách action của quyền  


        public List<string> UnitUsingIds { get; set; } = new List<string>(); // Đơn vị sử dụng quyền được tạo string
        
        public UnitRoleShort() {}


        public UnitRoleShort(string Id, string Name,string Code,
            bool IsSpecialUnit, bool isOnlySeeMe,
            List<string> donViIds , List<string> listMenu , List<DonViShort> donVis
            , List<string> UnitUsingIds , List<DonViShort> UnitUsing ,List<string> listAction , int level
            )
        {
            this.Id = Id;
            this.Name = Name;
            this.Code = Code;
            this.IsSpecialUnit = IsSpecialUnit;
            this.IsOnlySeeMe = isOnlySeeMe;
            this.DonViIds = donViIds != null ? donViIds : null;
            this.ListAction = listAction;
            this.ListMenu = listMenu;
            this.DonVis = donVis != null ? donVis : null;
            this.UnitUsing = UnitUsing != null ? UnitUsing : null;
            this.UnitUsingIds = UnitUsingIds != null ? UnitUsingIds : null;
            this.Level = level;
        }
        public UnitRoleShort(UnitRole unitRole)
        {
            this.Id = unitRole.Id;
            this.Code = unitRole.Code;
            this.Name = unitRole.Name;
            this.IsSpecialUnit = IsSpecialUnit;
            this.IsOnlySeeMe = unitRole.IsOnlySeeMe;
            this.DonViIds = unitRole.DonViIds;
            this.ListAction = unitRole.ListAction;
            this.ListMenu = unitRole.ListMenu;
            this.DonVis = unitRole.DonVis;
            this.UnitUsing = unitRole.UnitUsing;
            this.UnitUsingIds = unitRole.UnitUsingIds;
            this.Level = unitRole.Level;
        }
        
        
    }
    
    
    
    
    public class UnitRoleShortShow
    {
        public  string Id { get; set; }
        
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsSpecialUnit { get; set; }

        public bool IsOnlySeeMe { get; set; } = true;
        
        public int Level { get; set; }
        
        public List<DonViShort> DonVis { get; set; } = new List<DonViShort>();// Danh Sach ID don vi 

        public List<DonViShort> UnitUsing { get; set; } = new List<DonViShort>();// Đơn vị sử dụng quyền được tạo
        
        
        public UnitRoleShortShow() {}

        
        public UnitRoleShortShow(UnitRole unitRole)
        {
            this.Id = unitRole.Id;
            this.Code = unitRole.Code;
            this.Name = unitRole.Name;
            this.IsSpecialUnit = IsSpecialUnit;
            this.IsOnlySeeMe = unitRole.IsOnlySeeMe;
            this.DonVis = unitRole.DonVis;
            this.UnitUsing = unitRole.UnitUsing;
            this.Level = unitRole.Level;
        }
        
        
    }
    
    

    
}