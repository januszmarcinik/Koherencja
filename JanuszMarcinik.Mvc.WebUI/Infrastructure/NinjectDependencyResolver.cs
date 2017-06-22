﻿using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Concrete;
using JanuszMarcinik.Mvc.Domain.Identity.Context;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ApplicationIdentityDbContext>().ToSelf().InRequestScope();

            kernel.Bind<IAnswersRepository>().To<AnswersRepository>().InSingletonScope();
            kernel.Bind<IDictionariesRepository>().To<DictionariesRepository>().InSingletonScope();
            kernel.Bind<IIntervieweesRepository>().To<IntervieweesRepository>().InSingletonScope();
            kernel.Bind<IQuestionnairesRepository>().To<QuestionnairesRepository>().InSingletonScope();
            kernel.Bind<IQuestionsRepository>().To<QuestionsRepository>().InSingletonScope();
            kernel.Bind<IResultsRepository>().To<ResultsRepository>().InSingletonScope();
        }
    }
}