using Apbd5.DTOs;
using Apbd5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Apbd5.Controllers;

[ApiController]
[Route("api/pcs")]
public class PCsController(IPcService pcService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPcs()
    {
        return Ok(await pcService.GetAllPcsAsync());
    }

    [HttpGet("{id:int}/components")]
    public async Task<IActionResult> GetPcComponents(int id)
    {
        return await pcService.GetPcComponentsAsync(id) switch
        {
            null => NotFound(),
            var components => Ok(components)
        };
    }

    [HttpPost]
    public async Task<IActionResult> CreatePc(CreatePcRequest request)
    {
        var pc = await pcService.CreatePcAsync(request);
        return Created($"api/pcs/{pc.id}", pc);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePc(int id, PutPcRequest request)
    {
        return await pcService.UpdatePcAsync(id, request) switch
        {
            true => Ok(),
            false => NotFound()
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePc(int id)
    {
        return await pcService.DeletePcAsync(id) switch
        {
            true => NoContent(),
            false => NotFound()
        };
    }
}