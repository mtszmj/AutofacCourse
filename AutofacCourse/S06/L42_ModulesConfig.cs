using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace AutofacCourse.S06
{
    class L42_ModulesConfig
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            Console.WriteLine($"{new CalculationModule().GetType().FullName}");

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "\\S06")
                .AddJsonFile("config_module.json");

            var configuration = configBuilder.Build();

            var containerBuilder = new ContainerBuilder();
            var configModule = new ConfigurationModule(configuration);

            containerBuilder.RegisterModule(configModule);

            using (var container = containerBuilder.Build())
            {
                float a = 3, b = 4;

                foreach (IOperation op in container.Resolve<IList<IOperation>>())
                {
                    Console.WriteLine($"{op.GetType().Name} of {a} and {b} = {op.Calculate(a, b)}");
                }
            }

        }
    }

    public class CalculationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Addition>().As<IOperation>();
            builder.RegisterType<Multiplication>().As<IOperation>();
        }
    }
}
