using Application.Dto_s.Responses;
using Application.Repositories;
using Application.UseCases.Queries;
using Domain.Contracts.CleanModels;
using Domain.Contracts.GetModels;
using Mapster;
using Mediator;

namespace Application.UseCases.QueryHandlers;

public class GetByFilterQueryHandler : IQueryHandler<GetByFilterQuery, IEnumerable<GetByFilterResponse>>
{
    private readonly ICustomRequestsRepository _customRequestsRepository;

    public GetByFilterQueryHandler(ICustomRequestsRepository customRequestsRepository)
    {
        _customRequestsRepository = customRequestsRepository;
    }

    public async ValueTask<IEnumerable<GetByFilterResponse>> Handle(GetByFilterQuery query, CancellationToken cancellationToken)
    {
        var requestData = query.Adapt<GetByFilterDto>();
        
        var result = await _customRequestsRepository.GetByFilter(requestData, cancellationToken);
        
        var response = result.Adapt<IEnumerable<GetByFilterResponse>>();

        return response;
    }
}