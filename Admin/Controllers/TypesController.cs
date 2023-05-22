using Admin.Models.Interface;
using Admin.Models.ViewModels;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class TypesController : Controller
    {
        private ITypes _Types;
        public TypesController(ITypes types)
        {
            _Types = types;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ReturnResponse> AddTypes([FromBody] TypesVM types)
        {
            ReturnResponse response = new ReturnResponse();
            Types type;
            foreach (var item in types.Types)
            {
                type = new Types
                {
                    ProductsId = types.ProductId,
                    Value = item
                };
                await _Types.CreateType(type);
            }
            response.IsSuccess = true;
            response.Status = Status.Success;
            response.Code = "200";

            return response;
        }

        public async Task<ReturnResponse> GetAllTypesByProductId(Guid ProductId)
        {
            ReturnResponse response = new ReturnResponse();
            await _Types.GetTypeByProductId(ProductId);
            return response;
        }
    }
}
