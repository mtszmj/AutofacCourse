using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S02
{
    public class L09_ChoiceOfConstructors
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterType<EmailLog>().As<ILog>();
            builder.RegisterType<ConsoleLog>()
                .As<ILog>()
                .As<IConsole>().PreserveExistingDefaults();
            builder.RegisterType<Engine>();
            builder.RegisterType<Car>()
                .UsingConstructor(typeof(Engine)); // <-- wywola konstruktor tylko z silnikiem

            IContainer container = builder.Build();
            var car = container.Resolve<Car>();
            car.Go();

        }
    }
}
