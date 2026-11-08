using IoTPlatform.Application.DTOs.Devices;
using IoTPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.Api.Controllers;

[ApiController]
[Route("api/devices")]
[Authorize]
public class DevicesController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DevicesController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var devices = await _deviceService.GetAllAsync();
        return Ok(devices);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var device = await _deviceService.GetByIdAsync(id);
        if (device is null)
            return NotFound(new { message = $"Device with id {id} not found." });

        return Ok(device);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDeviceRequest request)
    {
        try
        {
            var device = await _deviceService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = device.Id }, device);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDeviceRequest request)
    {
        var updatedDevice = await _deviceService.UpdateAsync(id, request);
        if (updatedDevice is null)
            return NotFound(new { message = $"Device with id {id} not found." });

        return Ok(updatedDevice);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _deviceService.DeleteAsync(id);
        if (!success)
            return NotFound(new { message = $"Device with id {id} not found." });

        return NoContent();
    }
}
