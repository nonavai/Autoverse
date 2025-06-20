using Application.Repositories;
using Application.Repositories.VIew;
using Domain.Entities;
using Domain.Entities.CustomEntities;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class ModificationViewRepository : BaseGuidRepository<ModificationView>, IModificationViewRepository 
{
    private AutoVerseContext _dataBase;
    
    public ModificationViewRepository(AutoVerseContext dataBase) : base(dataBase)
    {
        _dataBase = dataBase;
    }
}