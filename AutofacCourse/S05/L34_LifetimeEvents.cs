using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;

namespace AutofacCourse.S05
{
    class L34_LifetimeEvents
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterType<Parent>();
            builder.RegisterType<Child>()
                .OnActivating(a =>
                {
                    Console.WriteLine("Child OnActivating");
                    a.Instance.Parent = a.Context.Resolve<Parent>();

                    //a.ReplaceInstance(new BadChild());
                })
                .OnActivated(a => { Console.WriteLine("Child OnActivated"); })
                .OnRelease(a => { Console.WriteLine("Child OnRelease"); })
                .OnPreparing(a => { Console.WriteLine("Child OnPreparing"); })
                .OnRegistered(a => { Console.WriteLine("Child OnRegistered"); });

            using (var scope = builder.Build())
            {
                var child = scope.Resolve<Child>();
                var parent = child.Parent;
                Console.WriteLine(parent);
                Console.WriteLine(child);
            }
        }
    }
}
