using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.Indexed;

namespace AutofacCourse.S04
{
    class L27_KeyedServiceLookup
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().Keyed<ILog>("cmd");
            builder.Register<SMSLog>(c => new SMSLog("123456")).Keyed<ILog>("sms");
            builder.RegisterType<Reporting>();

            using (var c = builder.Build())
            {
                c.Resolve<Reporting>().Report();
            }
        }

        public class Reporting
        {
            private IIndex<string, ILog> logs;

            public Reporting(IIndex<string, ILog> logs)
            {
                this.logs = logs ?? throw new ArgumentNullException(nameof(logs));
            }

            public void Report()
            {
                logs["sms"].Write("Starting report output");
            }
        }

        public class Settings
        {
            public string LogMode { get; set; }
        }
    }
}
