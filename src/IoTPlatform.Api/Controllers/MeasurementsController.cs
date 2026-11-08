using IoTPlatform.Application.DTOs.Measurements;
using IoTPlatform.Application.Interfaces;
using IoTPlatform.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.Api.Controllers;

[ApiController]
[Route("api/devices/{deviceId:int}/measurements")]
[Authorize]
public class MeasurementsController : ControllerBase
{
    private readonly IMeasurementService _measurementService;

    public MeasurementsController(IMeasurementService measurementService)
    {
        _measurementService = measurementService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMeasurements(
        int deviceId,
        [FromQuery] MetricType? metricType = null,
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null,
        [FromQuery] int limit = 100)
    {
        var measurements = await _measurementService.GetByDeviceIdAsync(deviceId, metricType, from, to, limit);
        return Ok(measurements);
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestMeasurements(int deviceId)
    {
        var latestMeasurements = await _measurementService.GetLatestByDeviceIdAsync(deviceId);
        return Ok(latestMeasurements);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int deviceId, [FromBody] CreateMeasurementRequest request)
    {
        try
        {
            var measurement = await _measurementService.CreateAsync(deviceId, request);
            return CreatedAtAction(nameof(GetMeasurements), new { deviceId }, measurement);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
