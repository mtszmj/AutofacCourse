using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    class L40_MSConfig_JsonXml
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "\\S06")
                .AddJsonFile("config.json");

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

    public interface IOperation
    {
        float Calculate(float a, float b);
    }

    public class Addition : IOperation
    {
        public float Calculate(float a, float b)
        {
            return a + b;
        }
    }

    public class Multiplication : IOperation
    {
        public float Calculate(float a, float b)
        {
            return a * b;
        }
    }
}
