using Application.Caching;
using Application.Dto_s.Responses;
using Application.Repositories;
using Application.UseCases.Queries;
using Domain.Contracts.GetModels;
using Domain.Enums;
using Mapster;
using Mediator;

namespace Application.UseCases.QueryHandlers;

public class GetRandomCarsQueryHandler : IQueryHandler<GetRandomCarsQuery, IEnumerable<GetRandomCarsResponse>>
{
    private readonly ICustomRequestsRepository _customRequestsRepository;
    private readonly IViewCounterService _viewCounterService;

    public GetRandomCarsQueryHandler(ICustomRequestsRepository customRequestsRepository, IViewCounterService viewCounterService)
    {
        _customRequestsRepository = customRequestsRepository;
        _viewCounterService = viewCounterService;
    }

    public async ValueTask<IEnumerable<GetRandomCarsResponse>> Handle(GetRandomCarsQuery query, CancellationToken cancellationToken)
    {
        var requestData = query.Adapt<GetRandomCarsDto>();
        
        var result = await _customRequestsRepository.GetRandomCars(requestData, cancellationToken);

        await _viewCounterService.IncrementViewCountAsync(ViewType.Random);
        
        var response = result.Adapt<IEnumerable<GetRandomCarsResponse>>();

        return response;
    }
}