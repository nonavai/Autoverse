using Application.Dto_s.Responses;
using Application.Repositories;
using Application.UseCases.Queries;
using Domain.Contracts.GetModels;
using Mapster;
using Mediator;

namespace Application.UseCases.QueryHandlers;

public class GetConfigurationFullInfoQueryHandler : IQueryHandler<GetConfigurationFullInfoQuery, GetConfigurationFullInfoResponse>
{
    private readonly ICustomRequestsRepository _customRequestsRepository;

    public GetConfigurationFullInfoQueryHandler(ICustomRequestsRepository customRequestsRepository)
    {
        _customRequestsRepository = customRequestsRepository;
    }

    public async ValueTask<GetConfigurationFullInfoResponse> Handle(GetConfigurationFullInfoQuery query, CancellationToken cancellationToken)
    {
        var requestData = query.Adapt<GetConfigurationFullInfoDto>();
        
        var result = await _customRequestsRepository.GetConfigurationFullInfoAsync(requestData, cancellationToken);

        var response = result.Adapt<GetConfigurationFullInfoResponse>();

        return response;
    }
}