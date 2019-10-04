using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S02
{
    public static class L10_RegisteringInstances
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();

            var log = new ConsoleLog(); // instancja np. specyficzna dla testu
            builder.RegisterInstance(log).As<ILog>();

            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            IContainer container = builder.Build();
            var car = container.Resolve<Car>();
            car.Go();

        }
    }
}

