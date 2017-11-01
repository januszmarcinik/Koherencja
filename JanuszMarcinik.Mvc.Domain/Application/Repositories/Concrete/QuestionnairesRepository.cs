using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.Domain.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JanuszMarcinik.Mvc.Domain.Application.Keys;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Concrete
{
    public class QuestionnairesRepository : IQuestionnairesRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public Questionnaire GetById(long id)
        {
            return context.Questionnaires.Find(id);
        }

        public IEnumerable<Questionnaire> GetList()
        {
            return context.Questionnaires;
        }

        public Questionnaire Create(Questionnaire entity)
        {
            entity.OrderNumber = context.Questionnaires.Count() + 1;

            context.Questionnaires.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public Questionnaire Update(Questionnaire entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity;
        }

        public void Delete(long id)
        {
            var entity = GetById(id);
            context.Questionnaires.Remove(entity);
            context.SaveChanges();
        }

        public Questionnaire GetByType(KeyType keyType)
        {
            return context.Questionnaires
                .FirstOrDefault(x => x.KeyType == keyType);
        }
    }
}