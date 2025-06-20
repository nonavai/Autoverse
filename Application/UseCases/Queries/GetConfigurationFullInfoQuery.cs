using Application.Dto_s.Responses;
using Mediator;

namespace Application.UseCases.Queries;

public record GetConfigurationFullInfoQuery(string CarConfigurationId) : IQuery<GetConfigurationFullInfoResponse>;