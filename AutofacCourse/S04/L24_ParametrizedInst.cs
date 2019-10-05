using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S04
{
    class L24_ParametrizedInst
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
            }
        }

        public class Reporting
        {
            private Func<ConsoleLog> consoleLog;
            private Func<string, SMSLog> smsLog;

            public Reporting(Func<ConsoleLog> consoleLog, Func<string, SMSLog> smsLog) 
            {
                this.consoleLog = consoleLog;
                this.smsLog = smsLog;
            }

            public void Report()
            {
                consoleLog().Write("Reporting to console");
                consoleLog().Write("and again");

                smsLog("1233456abc").Write("texting admins");
            }
        }
    }
}
