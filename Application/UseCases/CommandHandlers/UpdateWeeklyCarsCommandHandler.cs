using Application.Caching;
using Application.Dto_s.Responses;
using Application.Repositories;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Domain.Contracts.GetModels;
using Domain.Enums;
using Mapster;
using Mediator;

namespace Application.UseCases.CommandHandlers;

public class UpdateWeeklyCarsCommandHandler: ICommandHandler<UpdateWeeklyCarsCommand, IEnumerable<UpdateWeeklyCarsResponse>>
{
    private readonly ICustomRequestsRepository _customRequestsRepository;
    private readonly IViewCounterService _viewCounterService;
    private readonly ICacheService _cacheService;

    public UpdateWeeklyCarsCommandHandler(ICustomRequestsRepository customRequestsRepository, IViewCounterService viewCounterService, ICacheService cacheService)
    {
        _customRequestsRepository = customRequestsRepository;
        _viewCounterService = viewCounterService;
        _cacheService = cacheService;
    }

    public async ValueTask<IEnumerable<UpdateWeeklyCarsResponse>> Handle(UpdateWeeklyCarsCommand query, CancellationToken cancellationToken)
    {
        var requestData = query.Adapt<WeeklyCarsDto>();

        await _cacheService.SetAsync<WeeklyCarsDto>(CacheKeys.WeeklyCars, requestData);

        var result = await _customRequestsRepository.GetWeeklyCarAsync(requestData, cancellationToken);
        
        var response = result.Adapt<IEnumerable<UpdateWeeklyCarsResponse>>();
        
        return response;
    }
}