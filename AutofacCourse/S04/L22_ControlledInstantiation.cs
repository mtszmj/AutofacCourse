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
    class L22_ControlledInstantiation
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>();
            builder.RegisterType<Reporting>();

            using (var c = builder.Build())
            {
                c.Resolve<Reporting>().ReportOnce();
                Console.WriteLine("Done reporting");
                //c.Resolve<Reporting>().ReportOnce();
            }
        }

        public class Reporting
        {
            private Owned<ConsoleLog> log;

            public Reporting(Owned<ConsoleLog> log)
            {
                if (log == null) throw new ArgumentNullException(nameof(log));
                this.log = log;
                Console.WriteLine("Reporting component created");
            }

            public void ReportOnce()
            {
                log.Value.Write("Log started");
                log.Dispose();
            }
        }
    }
}
