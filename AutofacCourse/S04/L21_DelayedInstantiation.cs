using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S04
{
    class L21_DelayedInstantiation
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");


            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>();
            builder.RegisterType<Reporting>();

            using (var c = builder.Build())
            {
                c.Resolve<Reporting>().Report();
                Console.WriteLine("Done reporting");
            }
        }

        public class Reporting
        {
            private Lazy<ConsoleLog> log;

            public Reporting(Lazy<ConsoleLog> log)
            {
                if (log == null) throw new ArgumentNullException(nameof(log));
                this.log = log;
                Console.WriteLine("Reporting component created");
            }

            public void Report()
            {
                log.Value.Write("Log started");
            }
        }
    }
}

