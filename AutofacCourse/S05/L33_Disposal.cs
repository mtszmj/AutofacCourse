using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace AutofacCourse.S05
{
    class L33_Disposal
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(new ConsoleLog());
            //builder.RegisterType<ConsoleLog>()
            //    .ExternallyOwned();
            //var container = builder.Build();
            using (var container = builder.Build()) {  
            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve<ConsoleLog>();
            }
            }
        }
    }
}
