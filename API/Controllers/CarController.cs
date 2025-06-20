using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/car")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> GetByFilter([FromQuery] GetByFilterQuery query, CancellationToken token = default)
    {
        
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
    
    [HttpGet]
    [Route("configuration")] 
    public async Task<IActionResult> GetConfigurationFullInfo([FromQuery] GetConfigurationFullInfoQuery query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
    
    [HttpGet]
    [Route("modifications")]
    public async Task<IActionResult> GetModificationsInfo([FromQuery] GetModificationsInfoQuery query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
    
    [HttpGet]
    [Route("random")]
    public async Task<IActionResult> GetRandomCars([FromQuery] GetRandomCarsQuery query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
    
    [HttpGet]
    [Route("weekly")]
    public async Task<IActionResult> GetWeeklyCars([FromQuery] GetWeeklyCarsQuery query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
    
    [HttpPut]
    [Route("weekly")]
    public async Task<IActionResult> UpdateWeeklyCars([FromBody] UpdateWeeklyCarsCommand query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
}