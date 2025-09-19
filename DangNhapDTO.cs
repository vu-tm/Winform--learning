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