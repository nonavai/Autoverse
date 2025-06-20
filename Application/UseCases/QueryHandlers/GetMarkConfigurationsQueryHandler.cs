using Application.Dto_s.Responses;
using Application.Repositories;
using Application.UseCases.Queries;
using Domain.Contracts.GetModels;
using Mapster;
using Mediator;

namespace Application.UseCases.QueryHandlers;

public class GetMarkConfigurationsQueryHandler : IQueryHandler<GetMarkConfigurationsQuery, GetMarkConfigurationsResponse>
{
    private readonly ICustomRequestsRepository _customRequestsRepository;

    public GetMarkConfigurationsQueryHandler(ICustomRequestsRepository customRequestsRepository)
    {
        _customRequestsRepository = customRequestsRepository;
    }

    public async ValueTask<GetMarkConfigurationsResponse> Handle(GetMarkConfigurationsQuery query, CancellationToken cancellationToken)
    {
        var requestData = query.Adapt<GetMarkConfigurationsDto>();
        
        var result = await _customRequestsRepository.GetMarkConfigurationsAsync(requestData, cancellationToken);

        var response = result.Adapt<GetMarkConfigurationsResponse>();

        return response;
    }
}