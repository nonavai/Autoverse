using Application.UseCases.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/marks")]
[ApiController]
public class MarkController: ControllerBase
{
    private readonly IMediator _mediator;

    public MarkController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMarks(CancellationToken token = default)
    {
        var response = await _mediator.Send(new GetMarksQuery() , token);
        
        return Ok(response);
    }
    
    [HttpGet]
    [Route("configurations")]
    public async Task<IActionResult> GetMarkConfigurations([FromQuery] GetMarkConfigurationsQuery query, CancellationToken token = default)
    {
        var response = await _mediator.Send(query, token);
        
        return Ok(response);
    }
}