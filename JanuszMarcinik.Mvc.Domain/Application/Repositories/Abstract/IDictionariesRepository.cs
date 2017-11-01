using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface IDictionariesRepository
    {
        BaseDictionary GetById(long id);
        IEnumerable<BaseDictionary> GetList();
        BaseDictionary Create(BaseDictionary entity);
        BaseDictionary Update(BaseDictionary entity);
        void Delete(long id);
        string GetValueOrEmptyIfNull(int? id);
    }
}