using Application.Caching;
using Application.Dto_s.Responses;
using Application.Repositories;
using Application.UseCases.Queries;
using Domain.Contracts.GetModels;
using Domain.Enums;
using Mapster;
using Mediator;

namespace Application.UseCases.QueryHandlers;

public class GetWeeklyCarsQueryHandler : IQueryHandler<GetWeeklyCarsQuery, IEnumerable<GetWeeklyCarsResponse>>
{
    private readonly ICustomRequestsRepository _customRequestsRepository;
    private readonly IViewCounterService _viewCounterService;
    private readonly ICacheService _cacheService;

    public GetWeeklyCarsQueryHandler(ICustomRequestsRepository customRequestsRepository, IViewCounterService viewCounterService, ICacheService cacheService)
    {
        _customRequestsRepository = customRequestsRepository;
        _viewCounterService = viewCounterService;
        _cacheService = cacheService;
    }

    public async ValueTask<IEnumerable<GetWeeklyCarsResponse>> Handle(GetWeeklyCarsQuery query, CancellationToken cancellationToken)
    {
        var requestData = query.Adapt<WeeklyCarsDto>();

        await _cacheService.GetAsync<WeeklyCarsDto>(CacheKeys.WeeklyCars);
        
        var result = await _customRequestsRepository.GetWeeklyCarAsync(requestData, cancellationToken);

        await _viewCounterService.IncrementViewCountAsync(ViewType.Weekly);
        
        var response = result.Adapt<IEnumerable<GetWeeklyCarsResponse>>();

        return response;
    }
}