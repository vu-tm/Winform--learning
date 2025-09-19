namespace QLSinhVien
{
    public partial class mainFrm : Form
    {
        Datatable dt = new DataTable();
        public mainFrm()
        {
            InitializeComponent();
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM DangNhap"; // Câu lệnh SQL để lấy tất cả dữ liệu từ bảng DangNhap
            dt.Clear(); // Bỏ đi dữ liệu cũ của datatable
            dt = DataProvider.LoadCSDL(query); // Load dữ liệu từ database vào datatable để lấy dữ liệu mới
            dataGridView1.DataSource = dt; // Gán datatable vào datasource của datagridview để hiển thị dữ liệu
        }
    }
}