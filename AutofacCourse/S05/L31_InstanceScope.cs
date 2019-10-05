using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S05
{
    class L31_InstanceScope
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>()
                //.InstancePerDependency() // default - za kazdym razem nowy komponent przy "resolve"
                //.SingleInstance() // singleton
                //.InstancePerLifetimeScope()
                //.InstancePerMatchingLifetimeScope("foo")
                //.InstancePerRequest()
                //.InstancePerOwned()
                ;

            var container = builder.Build();

            //using (var scope = container.BeginLifetimeScope())
            //{
            //    var log = scope.Resolve<ILog>();
            //    log.Write("Testing!");

            //    scope.Resolve<ILog>().Write("Second test");
            //}

            using (var scope1 = container.BeginLifetimeScope("foo"))
            {
                for (int i = 0; i < 3; i++)
                {
                    scope1.Resolve<ILog>();
                }

                using (var scope2 = scope1.BeginLifetimeScope())
                {
                    scope2.Resolve<ILog>();
                }
            }

            //using (var scope2 = container.BeginLifetimeScope("foo"))
            //{
            //    for (int Id = 0; Id < 3; Id++)
            //    {
            //        scope2.Resolve<ILog>();
            //    }
            //}

        }
    }
}
