using Application.Dto_s.Responses;
using Mediator;

namespace Application.UseCases.Queries;

public record GetModificationsInfoQuery(IEnumerable<string> ModificationIds) : IQuery<IEnumerable<GetModificationInfoResponse>>;
