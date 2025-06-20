using Application.Dto_s.Responses;
using Mediator;
namespace Application.UseCases.Queries;

public record GetWeeklyCarsQuery() : IQuery<IEnumerable<GetWeeklyCarsResponse>>;
