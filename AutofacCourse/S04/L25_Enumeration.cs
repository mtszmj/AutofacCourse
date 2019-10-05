using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S04
{
    class L25_Enumeration
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>();
            builder.Register<SMSLog>(c => new SMSLog("+1234")).As<ILog>();
            builder.RegisterType<Reporting>();

            using (var c = builder.Build())
            {
                c.Resolve<Reporting>().Report();
                Console.WriteLine("Done reporting");
            }
        }

        public class Reporting
        {
            private IList<ILog> allLogs;

            public Reporting(IList<ILog> allLogs)
            {
                this.allLogs = allLogs;
            }

            public void Report()
            {
                foreach (var log in allLogs)
                {
                    log.Write($"Hello, this is {log.GetType().Name}");
                }
            }
        }
    }
}
