using Domain.Contracts.GetModels;
using Domain.Entities;
using Domain.Entities.CustomEntities;

namespace Application.Repositories;

public interface ICustomRequestsRepository
{
    public Task<IEnumerable<WeeklyCar>> GetWeeklyCarAsync(WeeklyCarsDto dto, CancellationToken token = default);
    
    public Task<WeeklyCar?> GetManyCarsAsync(int pageNumber, int pageSize, CancellationToken token = default);

    public Task<Mark?> GetMarkConfigurationsAsync(GetMarkConfigurationsDto dto,
        CancellationToken token = default);

    public Task<CarConfiguration?> GetConfigurationFullInfoAsync(GetConfigurationFullInfoDto dto,
        CancellationToken token = default);

    public Task<IEnumerable<Modification>> GetModificationsFullInfoAsync(GetModificationsInfoDto dto, 
        CancellationToken token = default);

    public Task<IEnumerable<Generation>> GetRandomCars(GetRandomCarsDto dto, CancellationToken token = default);

    public Task<IEnumerable<Mark>> GetByFilter(GetByFilterDto dto, CancellationToken token = default);
}