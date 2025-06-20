using Application.Dto_s.Responses;
using Application.Repositories;
using Application.UseCases.Queries;
using Domain.Contracts.GetModels;
using Mapster;
using Mediator;

namespace Application.UseCases.QueryHandlers;

public class GetMarksQueryHandler : IQueryHandler<GetMarksQuery, IEnumerable<GetMarksResponse>>
{
    private readonly IMarkRepository _markRepository;

    public GetMarksQueryHandler(IMarkRepository markRepository)
    {
        _markRepository = markRepository;
    }

    public async ValueTask<IEnumerable<GetMarksResponse>> Handle(GetMarksQuery query, CancellationToken cancellationToken)
    {
        var result = await _markRepository.GetAllMarksAsync(cancellationToken);

        var response = result.Adapt<IEnumerable<GetMarksResponse>>();

        return response;
    }
}