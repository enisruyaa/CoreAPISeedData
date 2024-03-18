using CoreAPISeedData_1Tekrar.Models.Categories.RequestModels;
using CoreAPISeedData_1Tekrar.Models.Categories.ResponseModels;
using CoreAPISeedData_1Tekrar.Models.ContextClasses;
using CoreAPISeedData_1Tekrar.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreAPISeedData_1Tekrar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        MyContext _db;

        public CategoryController(MyContext db)
        {
            _db = db;
        }

        #region OnemliNot
        /*
                API Sistemin'de bir API creation vardır.. Bir'de API consuming vardır : 

           -> API Creation : Bir Projenin bilgilerini dışarı açmak için API projesi yaratmaktır.

           -> API Consuming : Ulaşmak istediğimiz proje bilgilerine ulaşabilmek adına veya oradan gelen cevaba ulaşabilmek için karşı tarafın yazmış olduğu API'ı kullanmaktır..

        */
        #endregion

        // Eğer bir Action'a HTTPRequest tipini attribute olarak vermezseniz API ilgili Action isminin başında Get,Post,Put,Delete var mı diye bakacaktır.. Bunlardan birisini bulursa Request tipini o olarak algılayacaktır.. Bulamazsa'da hata verevektir.

        [HttpGet]
        public IActionResult GetCategories() 
        {
            List<CategoryResponseModel> categories = _db.Categories.Select(x => new CategoryResponseModel
            {
                ID = x.ID,
                CategoryName = x.CategoryName,
                Description = x.Description
            }).ToList();

            return Ok(categories);
        }

        [HttpGet("GetSpecific")]
        public async Task<IActionResult> GetCategory(int id) 
        { 
            CategoryResponseModel? category = await _db.Categories.
                Where(x => x.ID == id).
                Select(x => new CategoryResponseModel
                {
                    ID = x.ID,
                    CategoryName = x.CategoryName,
                    Description = x.Description
                }).FirstOrDefaultAsync();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestModel model)
        {
            Category c = new()
            {
                CategoryName = model.CategoryName,
                Description = model.Description 
            };

            await _db.Categories.AddAsync(c);
            await _db.SaveChangesAsync();

            return Ok(new CategoryResponseModel { ID = c.ID, CategoryName = c.CategoryName, Description = c.Description });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryRequestModel model)
        {
            Category? originalCategory = await _db.Categories.FindAsync(model.ID);
            originalCategory.CategoryName = model.CategoryName;
            originalCategory.Description = model.Description;
            await _db.SaveChangesAsync();
            return Ok(new CategoryResponseModel { ID = model.ID, CategoryName = model.CategoryName, Description = model.Description });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Category? originalCategory = await _db.Categories.FindAsync(id);
            _db.Categories.Remove(originalCategory);
            await _db.SaveChangesAsync();
            return Ok($"{originalCategory.CategoryName} İsimli Kategori Başarılı Bir Şekilde Silinmiştir");
        }
    }
}
