using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.ResolveAnything;

namespace AutofacCourse.S07
{
    class L45_RegistrationServices
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            using (var container = builder.Build())
            {
                container.Resolve<Person>().Speak();
            }

        }
    }

    public interface ICanSpeak
    {
        void Speak();
    }

    public class Person : ICanSpeak
    {
        public void Speak()
        {
            Console.WriteLine("Hello");
        }
    }
}
