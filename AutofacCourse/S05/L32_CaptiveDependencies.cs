using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S05
{
    class L32_CaptiveDependencies
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterType<ResourceManager>().SingleInstance();
            builder.RegisterType<SingletonResource>().As<IResource>().SingleInstance();
            builder.RegisterType<InstancePerDependencyResource>().As<IResource>().InstancePerDependency();

            using (var container = builder.Build())
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    scope.Resolve<ResourceManager>();
                }
            }
        }

        public interface IResource
        {

        }

        private class InstancePerDependencyResource : IResource, IDisposable
        {
            public InstancePerDependencyResource()
            {
                Console.WriteLine("Instance per dep created");
            }

            public void Dispose()
            {
                Console.WriteLine("Instance per dep destroyed");
            }
        }

        private class SingletonResource : IResource
        {
        }

        public class ResourceManager
        {
            public IEnumerable<IResource> Resources { get; set; }

            public ResourceManager(IEnumerable<IResource> resources)
            {
                Resources = resources ?? throw new ArgumentNullException(nameof(resources));
            }
        }
    }
}
