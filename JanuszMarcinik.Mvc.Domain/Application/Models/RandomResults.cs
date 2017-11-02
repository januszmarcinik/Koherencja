using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public class RandomResults
    {
        public List<RandomResultsInterviewees> Interviewees { get; set; }
        public List<RandomResultsQuestionnaires> Questionnaires { get; set; }

        public RandomResults(IEnumerable<Questionnaire> questionnaires, IEnumerable<BaseDictionary> dictionaries)
        {
            Questionnaires = new List<RandomResultsQuestionnaires>();
            foreach (var questionnaire in questionnaires)
            {
                Questionnaires.Add(new RandomResultsQuestionnaires(questionnaire));
            }

            Interviewees = new List<RandomResultsInterviewees>();
            foreach (DictionaryType dictionaryType in Enum.GetValues(typeof(DictionaryType)))
            {
                Interviewees.Add(new RandomResultsInterviewees()
                {
                    DictionaryType = dictionaryType,
                    ItemIds = dictionaries.Where(x => x.DictionaryType == dictionaryType).Select(x => x.BaseDictionaryId).ToList()
                });
            }
        }

        public int RandomInterviewee(DictionaryType dictionaryType)
        {
            var items = Interviewees.FirstOrDefault(x => x.DictionaryType == dictionaryType);
            var index = new Random().Next(0, items.ItemIds.Count);
            return items.ItemIds.ElementAt(index);
        }
    }

    public class RandomResultsInterviewees
    {
        public DictionaryType DictionaryType { get; set; }
        public List<int> ItemIds { get; set; }
    }

    public class RandomResultsQuestionnaires
    {
        public RandomResultsQuestionnaires(Questionnaire questionnaire)
        {
            QuestionnaireId = questionnaire.QuestionnaireId;
            Questions = new List<RandomResultsQuestions>();
            foreach (var question in questionnaire.Questions)
            {
                Questions.Add(new RandomResultsQuestions(question));
            }
        }

        public int QuestionnaireId { get; set; }
        public List<RandomResultsQuestions> Questions { get; set; }
    }

    public class RandomResultsQuestions
    {
        public RandomResultsQuestions(Question question)
        {
            QuestionId = question.QuestionId;
            AnswersIds = question.Answers.Select(x => x.AnswerId).ToList();
        }

        public int QuestionId { get; set; }
        public List<int> AnswersIds { get; set; }

        public int Random()
        {
            var index = new Random().Next(0, AnswersIds.Count);
            return AnswersIds.ElementAt(index);
        }
    }
}