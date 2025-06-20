using Application.Caching;
using Application.Dto_s.Responses;
using Application.Repositories;
using Application.UseCases.Queries;
using Domain.Contracts.GetModels;
using Domain.Enums;
using Mapster;
using Mediator;

namespace Application.UseCases.QueryHandlers;

public class GetModificationsInfoQueryHandler : IQueryHandler<GetModificationsInfoQuery, IEnumerable<GetModificationInfoResponse>>
{
    private readonly ICustomRequestsRepository _customRequestsRepository;
    private readonly IViewCounterService _viewCounterService;

    public GetModificationsInfoQueryHandler(ICustomRequestsRepository customRequestsRepository, IViewCounterService viewCounterService)
    {
        _customRequestsRepository = customRequestsRepository;
        _viewCounterService = viewCounterService;
    }

    public async ValueTask<IEnumerable<GetModificationInfoResponse>> Handle(GetModificationsInfoQuery query, CancellationToken cancellationToken)
    {
        var requestData = query.Adapt<GetModificationsInfoDto>();
        
        var result = await _customRequestsRepository.GetModificationsFullInfoAsync(requestData, cancellationToken);
        
        await _viewCounterService.IncrementViewsBulkAsync(ViewType.Modification, query.ModificationIds);

        var response = result.Adapt<IEnumerable<GetModificationInfoResponse>>();

        return response;
    }
}