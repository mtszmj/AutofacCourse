using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S03
{
    class L18_ScanningForModules
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(Program).Assembly);

            builder.RegisterAssemblyModules<ParentChildModule>(typeof(Program).Assembly);

            var container = builder.Build();
            Console.WriteLine(container.Resolve<Child>().Parent);
        }
    }
}
