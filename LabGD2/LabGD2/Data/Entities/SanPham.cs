using System.ComponentModel.DataAnnotations.Schema;

namespace LabGD2.Data.Entities
{
	public class SanPham
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Status { get; set; }
        public string IdNhaSanXuat { get; set; }
		[NotMapped]
		public bool IsEdited { get; set; }
		public NhaSanXuat NhaSanXuat { get; set; }
    }
}
