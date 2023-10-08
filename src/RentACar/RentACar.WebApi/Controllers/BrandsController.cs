using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Features.Brands.Commands.Create;

namespace RentACar.WebApi.Controllers;
public class BrandsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBrandCommand command)
    {
        var response = await Mediator.Send(command);
        return Created(string.Empty, response);
}