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