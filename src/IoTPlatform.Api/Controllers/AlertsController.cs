using IoTPlatform.Application.DTOs.Alerts;
using IoTPlatform.Application.Interfaces;
using IoTPlatform.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.Api.Controllers;

[ApiController]
[Route("api/alerts")]
[Authorize]
public class AlertsController : ControllerBase
{
    private readonly IAlertService _alertService;

    public AlertsController(IAlertService alertService)
    {
        _alertService = alertService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int? deviceId = null,
        [FromQuery] AlertSeverity? severity = null,
        [FromQuery] bool? isAcknowledged = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var alerts = await _alertService.GetAllAsync(deviceId, severity, isAcknowledged, pageNumber, pageSize);
        return Ok(alerts);
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnacknowledgedCount([FromQuery] int? deviceId = null)
    {
        var count = await _alertService.GetUnacknowledgedCountAsync(deviceId);
        return Ok(new { count });
    }

    [HttpPatch("{id:int}/acknowledge")]
    public async Task<IActionResult> Acknowledge(int id)
    {
        var alert = await _alertService.AcknowledgeAsync(id);
        if (alert is null)
            return NotFound(new { message = $"Alert with id {id} not found." });

        return Ok(alert);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAlertRequest request)
    {
        try
        {
            var alert = await _alertService.CreateAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = alert.Id }, alert);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
