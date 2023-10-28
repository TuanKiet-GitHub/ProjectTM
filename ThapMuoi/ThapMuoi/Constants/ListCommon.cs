

using ThapMuoi.Models.Core;

namespace ThapMuoi.Constants;

public class ListCommon
{
    public static List<ItemCommon> listCommon = new List<ItemCommon>
    {
        new ItemCommon { Code = "DM_MUCDICHTHUE", Name = "Danh mục mục đích thuê" },
            new ItemCommon { Code = "DM_LOAIDAT", Name = "Danh mục loại đất" },
            new ItemCommon { Code = "DM_LINHVUCKINHDOANH", Name = "Danh mục lĩnh vực kinh doanh" },
            new ItemCommon { Code = "DM_LOAIVANBAN", Name = "Danh mục loại văn bản" },
            new ItemCommon { Code = "DM_COQUANBANHANH", Name = "Danh mục cơ quan ban hành" },
            new ItemCommon { Code = "DM_KEUGOIDAUTUTHUCAP", Name = "Danh mục kêu gọi đầu tư thứ cấp" },
            new ItemCommon { Code = "DM_LOAIGIAYCHUNGNHAN", Name = "Danh mục loại giấy chứng nhận" },
            new ItemCommon { Code = "DM_LOAIGIAYPHEPHOATDONG", Name = "Danh mục loại giấy phép hoạt động" },
            new ItemCommon { Code = "DM_PHANLOAINUOCXA", Name = "Danh mục phân loại nước xả" },
            new ItemCommon { Code = "DM_PHUONGTHUCXATHAI", Name = "Danh mục phương thức xả thải" },
            new ItemCommon { Code = "DM_LOAIKHUNGGIA", Name = "Danh mục loại khung giá" },
            new ItemCommon { Code = "DM_LOAIQUYHOACHVAXAYDUNG", Name = "Danh mục loại quy hoạch và xây dựng" },
            new ItemCommon { Code = "DM_LOAIKHENTHUONG", Name = "Danh mục loại khen thưởng" },
            new ItemCommon { Code = "DM_LOAITUVAN", Name = "Danh mục loại tư vấn" },
            new ItemCommon { Code = "DM_PHUONGTHUCTHANHTOAN", Name = "Danh mục phương thức thanh toán" },
            new ItemCommon { Code = "DM_TRANGTHAI", Name = "Danh mục trạng thái" },
            new ItemCommon { Code = "DM_DANHGIA", Name = "Danh mục đánh giá" },
            new ItemCommon { Code = "DM_LOAIGIAYKHAITHAC", Name = "Danh mục loại giấy khai thác" },
            new ItemCommon { Code = "DM_GIOITINH", Name = "Danh mục giới tính" },
            new ItemCommon { Code = "DM_DANTOC", Name = "Danh mục dân tộc" },
            new ItemCommon { Code = "DM_QUOCTICH", Name = "Danh mục quốc tịch" }, /// Mới thêm 
            new ItemCommon { Code = "DM_UUDAIDAUTU", Name = "Danh mục ưu đãi đầu tư" }, // Mới thêm
            new ItemCommon { Code = "DM_VONDAUTU", Name = "Danh mục ưu đãi đầu tư" }, // Mới thêm
            new ItemCommon { Code = "DM_LOAINHADAUTU", Name = "Danh mục loại nhà đầu tư" }, // Mới thêm
    };
}