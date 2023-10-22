using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Features.Models.Queries.GetList;
using RentACar.Application.Features.Models.Queries.GetListByDynamic;

namespace RentACar.WebApi.Controllers;

public class ModelsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        var response = await Mediator.Send(new GetListModelQuery { PageRequest = pageRequest });
        return Ok(response);
    }

    [HttpGet("get-with-filter")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest, [FromQuery] DynamicQuery dynamicQuery)
    {
        var response = await Mediator.Send(new GetListByDynamicModelQuery { PageRequest = pageRequest, DynamicQuery = dynamicQuery });
        return Ok(response);
    }
}
