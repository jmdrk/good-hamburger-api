using GoodHamburger.Domain.Menu;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.Api.Controllers;

[ApiController]
[Route("api/menu")]
public sealed class MenuController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var menu = MenuCatalog.GetAll()
            .Select(item => new
            {
                code = item.Code.ToString(),
                name = item.Name,
                type = item.Type.ToString(),
                price = item.Price
            });

        return Ok(menu);
    }
}