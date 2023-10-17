using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Features.Models.Queries.GetList;

namespace RentACar.WebApi.Controllers;

public class ModelsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        var response = await Mediator.Send(new GetListModelQuery { PageRequest = pageRequest });
        return Ok(response);
    }
}
