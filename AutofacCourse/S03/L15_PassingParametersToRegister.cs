using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace AutofacCourse.S03
{
    public class L15_PassingParametersToRegister
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();

            //// Registration time
            // named parameter
            builder.RegisterType<SMSLog>()
                .As<ILog>()
                .WithParameter("phoneNumber", "+123456");

            // typed parameter
            builder.RegisterType<SMSLog>()
                .As<ILog>()
                .WithParameter(new TypedParameter(typeof(string), "+123456"));

            // resolve parameter
            builder.RegisterType<SMSLog>()
                .As<ILog>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "phoneNumber",
                        (pi, ctx) => "+123456"
                    )
                );

            var container = builder.Build();
            var log = container.Resolve<ILog>();
            log.Write("test message");

            //// Resolution time
            Random random = new Random();

            var builder2 = new ContainerBuilder();
            builder2.Register((c, p) => new SMSLog(p.Named<string>("phoneNumber"))).As<ILog>();
            var container2 = builder2.Build();
            log = container2.Resolve<ILog>(new NamedParameter("phoneNumber", random.Next().ToString()));
            log.Write("test message");
        }
    
    }
}
