using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S03
{
    class L17_ScanningForTypes
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();

            // rejestracja na podstawie przegladania calego assembly z dodatkowymi warunkami, np. wylaczeniem poszczegolnych typow
            // lub na podstawie customowej rejestracji
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Log"))
                .Except<SMSLog>()
                .Except<ConsoleLog>(c => c.As<ILog>().SingleInstance())
                .AsSelf();

            // rejestracja z assembly jako pierwszy z interfejsow
            builder.RegisterAssemblyTypes(assembly)
                .Except<SMSLog>()
                .Where(t => t.Name.EndsWith("Log"))
                .As(t => t.GetInterfaces()[0]);

            var container = builder.Build();

            var log = container.Resolve<ILog>();
            log.Write("scanning for types test");


        }
    }
}
