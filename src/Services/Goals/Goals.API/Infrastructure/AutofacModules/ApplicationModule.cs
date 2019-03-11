using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Goals.Domain.AggregatesModel.GoalsAggregate;
using Goals.Infrastructure.Repositories;

namespace Goals.API.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string queriesConnectionString)
        {
            QueriesConnectionString = queriesConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //TODO: Register queries

            builder.RegisterType<GoalsRepository>()
                .As<IGoalRepository>()
                .InstancePerLifetimeScope();

            //TODO: Register UsersRepository

            //TODO: Register Request manager

            //TODO: Register Integration Event Handler

        }
    }
}
