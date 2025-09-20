# Kết nối dữ liệu từ SQL

```cs
namespace QLSinhVien
{
    public class DataProvider
    {
        const string connString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"; // Chuỗi kết nối thực tế đến cơ sở dữ liệu

        private static MySqlConnection connection;
        // SqlConnection : lớp quản lý kết nối đến cơ sở dữ liệu MySQL
        // static chỉ rằng biến dùng chung cho tất cả các instance của lớp, không cần khởi tạo đối tượng để truy cập
        // private chỉ rằng biến chỉ có thể truy cập trong phạm vi lớp DataProvider

        public static void OpenConnection()
        {
            connection = new SqlConnection(connString); // Tạo đối tượng kết nối mới với chuỗi kết nối
            connection.Open(); // Mở kết nối đến database bằng Open()
        }

        public static void CloseConnection()
        {
            connection.Close();
        }
    }
}
```

# Sử dụng dữ liệu từ SQL
#### Khởi tạo lớp DTO
```cs
namespace QLSinhVien
{
    public class DangNhapDTO
    {
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Quyen { get; set; }

        public DangNhapDTO() { }
        public DangNhapDTO(string taiKhoan, string matKhau, string hoTen, string quyen)
        {
            TaiKhoan = taiKhoan;
            MatKhau = matKhau;
            HoTen = hoTen;
            Quyen = quyen;
        }
    }
}
```
#### Sử dụng dữ liệu
```cs
public static void GetAllDangNhap() // DAO
{
    BindingList<DangNhapDTO> dnhap = new BindingList<DangNhapDTO>(); // Hoặc dùng List<DangNhapDTO>
    try
    {
        OpenConnection(); // Mở kết nối đến database

        string query = "SELECT * FROM DangNhap"; // Câu lệnh SQL để lấy tất cả dữ liệu từ bảng DangNhap
        SqlCommand command = new SqlCommand(query, connection); // Tạo đối tượng SqlCommand để thực thi câu lệnh SQL

        SqlDataReader reader = command.ExecuteReader(); // Thực thi câu lệnh và lấy dữ liệu trả về dưới dạng SqlDataReader

        while (reader.Read()) // Đọc từng dòng dữ liệu (đọc khi còn dòng)
        {
            DangNhapDTO dangNhap = new DangNhapDTO
            {
                TaiKhoan = reader["TaiKhoan"].ToString(), // Lấy giá trị "TaiKhoan" trong SQL
                MatKhau = reader["MatKhau"].ToString(),
                HoTen = reader["HoTen"].ToString(),
                Quyen = reader["Quyen"].ToString()
            };
            dnhap.Add(dangNhap); // Thêm đối tượng DangNhapDTO vào danh sách
        }
    }
    catch (SqlException ex)
    {
        MessageBox.Show(ex.Message);
    }
    finally // Dù có lỗi hay không thì khối finally vẫn luôn được thực thi
    {
        CloseConnection(); // Đảm bảo đóng kết nối trong khối finally để tránh rò rỉ kết nối
    }
}
```

# Hiển thị dữ liệu từ CSDL
query -> lấy data từ csdl -> dataBase -> dataGridView
```cs
//Ở form cần hiển thị dữ liệu
        private void mainFrm_Load(object sender, EventArgs e)
        {
            // (Bước query)
            string query = "SELECT * FROM DangNhap"; // Câu lệnh SQL để lấy tất cả dữ liệu từ bảng DangNhap
            dt.Clear(); // Bỏ đi dữ liệu cũ của datatable
            // (Bước dataBase)
            dt = DataProvider.LoadCSDL(query); // Load dữ liệu từ database vào datatable để lấy dữ liệu mới
            // (Bước dataGridView)
            dataGridView1.DataSource = dt; // Gán datatable vào datasource của datagridview để hiển thị dữ liệu
        }
```

```cs
// Ở DataProvider.cs (lấy data từ csdl)
        public static DataTable LoadCSDL(string query)
        {
            DataTable dlieu = new DataTable(); // Tạo đối tượng DataTable để lưu trữ dữ liệu

            try
            {
                OpenConnection(); // Mở kết nối đến database

                SqlCommand command = new SqlCommand(query, connection); // Tạo đối tượng để thực thi câu lệnh SQL
                SqlDataAdapter da = new SqlDataAdapter(command); // Tạo đối tượng cầu nối giữa command và datatable

                da.Fill(dlieu); // Điền dữ liệu từ SqlDataAdapter vào DataTable
        // tạo command viết code -> da cầu nối giữa command và datatable -> fill da vào dlieu và cuối cùng return dlieu(database)
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection(); // Đảm bảo đóng kết nối trong khối finally để tránh rò rỉ kết nối
            }

            return dlieu; // Trả về DataTable chứa dữ liệu
        }
```

---


# Thêm mới 
```cs
private void btnAdd_Click(object sender, EventArgs e)
{
    // Tạo form AddNhanVienForm
    AddNhanVienForm addNV = new AddNhanVienForm();
    // Hiện form dưới dạng hộp thoại
    // Chương trình dừng ngay dòng này, chờ đóng form AddNhanVienForm mới tiếp tục
    addNV.ShowDialog();

    /* =========================================== *\ 
        DialogResult là thuộc tính có sẵn của Form.
            - DialogResult.None
            - DialogResult.OK
            - DialogResult.Cancel
            - DialogResult.Yes
            - DialogResult.No
            - DialogResult.Abort
            - DialogResult.Retry
            - DialogResult.Ignore
    \* =========================================== */ 
    if(addNV.DialogResult== DialogResult.OK) // Kiểm tra xem bấm nút OK chưa?
    {
        refreshDataGridView(nvBUS.getListNV()); // refresh bằng cách lấy danh sách nv mới nhất
        // Hiện thông báo thêm thành công (optional)
        AddSuccessNotification tb = new AddSuccessNotification();
        tb.Show();
    }
}   
```

##### refreshDataGridView
```cs
        private void refreshDataGridView(BindingList<NhanVienDTO> listRefresh) // Tải lại DataGridView
        {
            DGVNhanVien.Rows.Clear();

            foreach (NhanVienDTO nv in listRefresh)
            {
                if (nv.Trangthai == 1)
                {
                    string gioiTinh = nv.Gioitinh == 1 ? "Nam" : nv.Gioitinh == 2 ? "Nữ" : "Khác";
                    DGVNhanVien.Rows.Add(nv.Manv, nv.Tennv, gioiTinh, nv.Sdt
                   , nv.Ngaysinh.ToString("dd/MM/yyyy"), "Hoạt động");
                }
            }
            DGVNhanVien.ClearSelection();

        }
```

##### nvBUS
```cs 
private NhanVienBUS nvBUS = new NhanVienBUS();
```

##### getListNV
```cs
public BindingList<NhanVienDTO> getListNV() // (BUS)
{
    listNV = nvDAO.SelectAll();
    return listNV;
}
```

---

---

# Bài học rút ra:

##### -> DTO: Nơi định nghĩa cấu trúc dữ liệu
```cs
public class NhanVienDTO
{
    private int manv;
    private string tennv;
    //... các thuộc tính khác
    
    // Constructor và properties
    public override string ToString() { /* format hiển thị */ }
}
```
##### -> DAO: Xử lý thao tác với database
```cs
public class NhanVienDAO : DAOInterface<NhanVienDTO>
{
    public int Insert(NhanVienDTO t) { /* thêm mới */ }
    public int Update(NhanVienDTO t) { /* cập nhật */ }
    public int Delete(int t) { /* xóa mềm (set status = 0) */ }
    public BindingList<NhanVienDTO> SelectAll() { /* lấy tất cả */ }
    public NhanVienDTO SelectById(int t) { /* lấy theo ID */ }
}
```
##### -> BUS: Xử lý trung gian giữa GUI và DAO
```cs
public class NhanVienBUS
{
    private BindingList<NhanVienDTO> listNV;
    
    public Boolean removeNhanVien(int maNV) { /* xóa + cập nhật list */ }
    public Boolean insertNhanVien(NhanVienDTO NV) { /* thêm mới */ }
    public BindingList<NhanVienDTO> SearchNhanVien(string search) { /* tìm kiếm */ }
}
```
##### -> GUI: Hiển thị giao diện và tương tác
```cs
public partial class NhanVienGUI : Form
{
    private void NhanVienGUI_Load() { /* khởi tạo DataGridView */ }
    private void DGVNhanVien_CellPainting() { /* vẽ nút tùy chỉnh */ }
    private void refreshDataGridView() { /* làm mới dữ liệu */ }
}
```



