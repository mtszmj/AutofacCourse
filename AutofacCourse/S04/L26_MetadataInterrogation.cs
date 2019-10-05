using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.Metadata;

namespace AutofacCourse.S04
{
    class L26_MetadataInterrogation
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();

            // weekly typed
            builder.RegisterType<ConsoleLog>().WithMetadata("mode", "verbose");

            // strongly typed
            builder.RegisterType<ConsoleLog>().As<ILog>()
                .WithMetadata<Settings>(c => c.For(x => x.LogMode, "verbose"));
            
            builder.RegisterType<Reporting>();

            using (var c = builder.Build())
            {
                c.Resolve<Reporting>().Report();
                Console.WriteLine("Done reporting");
            }
        }

        public class Reporting
        {
            private Meta<ConsoleLog> log;
            private Meta<ILog, Settings> log2;


            public Reporting(Meta<ConsoleLog> log, Meta<ILog, Settings> log2)
            {
                if (log == null)
                {
                    throw new ArgumentNullException(nameof(log));
                }

                this.log = log;
                this.log2 = log2;
            }

            public void Report()
            {
                log.Value.Write("Starting report");

                if (log.Metadata["mode"] as string == "verbose")
                    log.Value.Write($"VERBOSE MODE: Logger started on {DateTime.Now}");

                if (log2.Metadata.LogMode as string == "verbose")
                    log.Value.Write($"VERBOSE MODE: Logger2 started on {DateTime.Now}");
            }
        }

        public class Settings
        {
            public string LogMode { get; set; }
        }
    }
}
