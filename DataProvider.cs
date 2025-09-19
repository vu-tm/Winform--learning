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

        public static void GetAllDangNhap()
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
    }
}

