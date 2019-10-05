using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S02
{
    public class L08_DefaultRegistrations
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            /* chcemy zmienic sposob logowania -> wystarczy przypisac inny typ ILoga */
            /* zeby zachowac dwa typy loggerow Id korzystac z pierwszego (email) trzeba dodac preserveexistingdefault.
             Mozna przypisac do dwoch interfejsow wywolujac kolejno As... As... */

            var builder = new ContainerBuilder();
            builder.RegisterType<EmailLog>().As<ILog>();
            builder.RegisterType<ConsoleLog>()
                .As<ILog>()
                .As<IConsole>().PreserveExistingDefaults();
            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            IContainer container = builder.Build();
            var car = container.Resolve<Car>();
            car.Go();
        }
    }
}
