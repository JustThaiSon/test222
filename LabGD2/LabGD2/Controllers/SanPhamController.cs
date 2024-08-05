using LabGD2.Data;
using LabGD2.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LabGD2.Controllers
{
	public class SanPhamController : Controller
	{
		private readonly MyDbContext _context;
		public SanPhamController(MyDbContext _context)
		{
			this._context = _context;
		}
		public IActionResult Index()
		{
			var GetAll = _context.SanPhams.Include(x => x.NhaSanXuat).ToList();
			var EditItemsJson = HttpContext.Session.GetString("EditItem");
			if (EditItemsJson != null)
			{
				var Edit = JsonConvert.DeserializeObject<List<SanPham>>(EditItemsJson);
				foreach (var item in Edit)
				{
					item.IsEdited = true;
					var existingItem = GetAll.FirstOrDefault(x => x.Id == item.Id);
					if (existingItem != null)
					{
						existingItem.IsEdited = true;
					}
					else
					{
						GetAll.Add(item);
					}
				}
			}
			return View(GetAll);
		}
		public IActionResult Create()
		{
			ViewBag.NhaSanXuat = _context.NhaSanXuats.ToList();
			return View();
		}
		[HttpPost]
		public IActionResult Create(SanPham rq)
		{

			_context.SanPhams.Add(rq);
			_context.SaveChanges();
			ViewBag.NhaSanXuat = _context.NhaSanXuats.ToList();
			return RedirectToAction("Index","Home");
		}

		[HttpGet]
		public IActionResult Details(string Id)
		{
			var Get = _context.SanPhams.Include(x=>x.NhaSanXuat).FirstOrDefault(x=>x.Id == Id);
			
			return View(Get);
		}
		[HttpGet]
		public IActionResult Delete(string Id)
		{
			var Get = _context.SanPhams.FirstOrDefault(x => x.Id == Id);
			_context.SanPhams.Remove(Get);
			_context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        public IActionResult Edit(string Id)
		{
			var Get = _context.SanPhams.FirstOrDefault(x => x.Id == Id);
			ViewBag.NhaSanXuat = _context.NhaSanXuats.ToList();
			return View(Get);
		}
		[HttpPost]
		public async Task< IActionResult> Edit(SanPham sp)
		{
			var Get = await _context.SanPhams.FirstOrDefaultAsync(x => x.Id == sp.Id);
			var EditItemJson = HttpContext.Session.GetString("EditItem");
			var EditItem = string.IsNullOrEmpty(EditItemJson) ? new List<SanPham>() : JsonConvert.DeserializeObject<List<SanPham>>(EditItemJson);
			Get.IsEdited = true;
			EditItem.Add(Get);
			HttpContext.Session.SetString("EditItem", JsonConvert.SerializeObject(EditItem));
			if (Get != null)
            {
				Get.Id = sp.Id;
				Get.Status = sp.Status;
				Get.IdNhaSanXuat = sp.IdNhaSanXuat;
				Get.Name = sp.Name;
				Get.Price = sp.Price;
				await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.NhaSanXuat = _context.NhaSanXuats.ToList();
			return RedirectToAction("Index");
        }
        public IActionResult RollBackEdit(string Id)
        {
            var EditJson = HttpContext.Session.GetString("EditItem");
            if (EditJson != null)
            {
                var EditItem = JsonConvert.DeserializeObject<List<SanPham>>(EditJson);
                var Product = EditItem.FirstOrDefault(x => x.Id == Id);
                var ProductDb = _context.SanPhams.FirstOrDefault(x => x.Id == Id);
                if (Product != null)
                {
                    ProductDb.Id = Product.Id;
                    ProductDb.Status = Product.Status;
                    ProductDb.IdNhaSanXuat = Product.IdNhaSanXuat;
                    ProductDb.Name = Product.Name;
                    ProductDb.Price = Product.Price;
                    _context.SaveChanges();
                    EditItem.Remove(Product);
                    HttpContext.Session.SetString("EditItem", JsonConvert.SerializeObject(EditItem));
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
