using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.Domain.Data;
using System.Collections.Generic;
using System.Data.Entity;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Concrete
{
    public class DictionariesRepository : IDictionariesRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public BaseDictionary GetById(long id)
        {
            return context.BaseDictionaries.Find(id);
        }

        public IEnumerable<BaseDictionary> GetList()
        {
            return context.BaseDictionaries;
        }

        public BaseDictionary Create(BaseDictionary entity)
        {
            context.BaseDictionaries.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public BaseDictionary Update(BaseDictionary entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity;
        }

        public void Delete(long id)
        {
            var entity = GetById(id);
            context.BaseDictionaries.Remove(entity);
            context.SaveChanges();
        }

        public string GetValueOrEmptyIfNull(int? id)
        {
            if (id.HasValue)
            {
                return GetById(id.Value)?.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}