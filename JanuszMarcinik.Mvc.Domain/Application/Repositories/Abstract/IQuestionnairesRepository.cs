using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface IQuestionnairesRepository
    {
        Questionnaire GetById(long id);
        IEnumerable<Questionnaire> GetList();
        Questionnaire Create(Questionnaire entity);
        Questionnaire Update(Questionnaire entity);
        void Delete(long id);
        Questionnaire GetFullModel(long questionnaireNumber);
    }
}