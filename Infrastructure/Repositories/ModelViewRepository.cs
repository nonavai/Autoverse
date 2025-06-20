using Application.Repositories;
using Application.Repositories.VIew;
using Domain.Entities;
using Domain.Entities.CustomEntities;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class ModelViewRepository : BaseGuidRepository<ModelView>, IModelViewRepository
{
    private AutoVerseContext _dataBase;
    public ModelViewRepository(AutoVerseContext dataBase) : base(dataBase)
    {
        _dataBase = dataBase;
    }
}