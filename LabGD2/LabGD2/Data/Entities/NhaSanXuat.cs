namespace LabGD2.Data.Entities
{
	public class NhaSanXuat
	{
        public string Id { get; set; }
        public string DiaChi { get; set; }
        public string Ten { get; set; }

        public List<SanPham> SanPham { get; set; }
    }
}
