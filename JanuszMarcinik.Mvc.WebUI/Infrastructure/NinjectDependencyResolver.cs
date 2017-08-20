using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Concrete;
using JanuszMarcinik.Mvc.Domain.Data;
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
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();

            kernel.Bind<IAnswersRepository>().To<AnswersRepository>().InRequestScope();
            kernel.Bind<IDictionariesRepository>().To<DictionariesRepository>().InRequestScope();
            kernel.Bind<IIntervieweesRepository>().To<IntervieweesRepository>().InRequestScope();
            kernel.Bind<IQuestionnairesRepository>().To<QuestionnairesRepository>().InRequestScope();
            kernel.Bind<IQuestionsRepository>().To<QuestionsRepository>().InRequestScope();
            kernel.Bind<IResultsRepository>().To<ResultsRepository>().InRequestScope();
            kernel.Bind<ICategoriesRepository>().To<CategoriesRepository>().InRequestScope();
        }
    }
}