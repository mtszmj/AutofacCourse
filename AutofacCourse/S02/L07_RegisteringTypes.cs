using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S02.L07
{
    public static class L07_RegisteringTypes
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>(); // Rejestracja typu
            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            IContainer container = builder.Build();
            var car = container.Resolve<Car>();
            car.Go();
        }
    }
}
