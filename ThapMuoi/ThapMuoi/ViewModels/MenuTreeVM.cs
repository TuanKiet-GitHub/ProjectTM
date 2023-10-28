using ThapMuoi.Models.Core;

namespace ThapMuoi.ViewModels
{
    public class MenuTreeVM
    {
     
        public MenuTreeVM(Menu model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Link = model.Path ?? "";
            this.Icon = model.Icon ?? "";
            this.ParentId = model.ParentId;
        }
        public string Id { get; set; }
        public string Name { get; set; }
   //     public string Text { get; set; }
        public List<MenuTreeVM> Children { get; set; }
  //      public List<MenuTreeVM> SubItems { get; set; } = new List<MenuTreeVM>();
    //    public State State { get; set; } = new State();
  //      public bool Opened { get; set; } = false;
        public string ParentId { get; set; }= "";
        public string Link { get; set; } = "";
        public string Icon { get; set; } = "";
       // public bool Selected { get; set; } = false;
    }
    public class MenuTreeVMGetAll
    {
        public MenuTreeVMGetAll(Menu model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Code = model.Resource; 
            this.CapDV = model.Level;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public int CapDV { get; set; }
        
    }
    
    
}