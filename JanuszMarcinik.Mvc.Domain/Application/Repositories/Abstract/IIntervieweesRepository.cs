using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface IIntervieweesRepository
    {
        Interviewee GetById(int id);
        Interviewee Create(Interviewee entity);
        IEnumerable<Interviewee> GetList(DateTime? dateFrom = null, DateTime? dateTo = null, int? ageId = null, int? sexId = null, 
            int? educationId = null, int? martialStatusId = null, int? materialStatusId = null, int? placeOfResidenceId = null, int? seniorityId = null);
        void Delete(int id);
    }
}