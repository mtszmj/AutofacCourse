using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.OwnedInstances;

namespace AutofacCourse.S04
{
    class L23_DynamicInst
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>();
            builder.RegisterType<SMSLog>();
            builder.RegisterType<Reporting>();

            using (var c = builder.Build())
            {
                c.Resolve<Reporting>().Report();
                Console.WriteLine("Done reporting");
                //c.Resolve<Reporting>().ReportOnce();
            }
        }

        public class Reporting
        {
            private Func<ConsoleLog> consoleLog;

            public Reporting(Func<ConsoleLog> consoleLog)
            {
                this.consoleLog = consoleLog;
            }

            public void Report()
            {
                consoleLog().Write("Reporting to console");
                consoleLog().Write("and again");
            }

        }
    }
}
