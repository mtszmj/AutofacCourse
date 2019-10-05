using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S05
{
    class L35_Startup
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterType<MyClass>()
                .AsSelf()
                .As<IStartable>()
                //.SingleInstance()
                .InstancePerDependency()
                ;

            var container = builder.Build();
            var id = container.Resolve<MyClass>().Id;
            Console.WriteLine(id);

        }

        public class MyClass : IStartable
        {
            public int Id;

            public MyClass()
            {
                Id = (new Random()).Next();
                Console.WriteLine($"MyClass ctor {Id}");
            }

            public void Start()
            {
                Console.WriteLine("Container being built");
            }
        }
    }
}
