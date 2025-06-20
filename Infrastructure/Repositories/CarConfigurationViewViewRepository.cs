using Application.Repositories;
using Application.Repositories.VIew;
using Domain.Entities;
using Domain.Entities.CustomEntities;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class CarConfigurationViewViewRepository : BaseGuidRepository<CarConfigurationView>, ICarConfigurationViewRepository
{
    private AutoVerseContext _dataBase;
    public CarConfigurationViewViewRepository(AutoVerseContext dataBase) : base(dataBase)
    {
        _dataBase = dataBase;
    }
}