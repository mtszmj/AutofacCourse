using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S02
{
    public static class L11_Lambda
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            
            builder.RegisterType<ConsoleLog>().As<ILog>();

            builder.RegisterType<Engine>();
            builder.Register((IComponentContext c) => new Engine(c.Resolve<ILog>(), 123));

            builder.RegisterType<Car>();
             
            IContainer container = builder.Build();
            var car = container.Resolve<Car>();
            car.Go();

        }
    }
}
