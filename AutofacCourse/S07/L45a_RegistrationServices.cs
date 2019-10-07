using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Autofac.Features.ResolveAnything;

namespace AutofacCourse.S07
{
    class L45a_RegistrationServices
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterType<HandlerFactory>().As<IHandlerFactory>();
            builder.RegisterSource(new HandlerRegistrationSource());
            builder.RegisterType<ConsumerA>();
            builder.RegisterType<ConsumerB>();

            using (var container = builder.Build())
            {
                container.Resolve<ConsumerA>().DoWork();
                container.Resolve<ConsumerB>().DoWork();
            }

        }

        public class HandlerRegistrationSource : IRegistrationSource
        {
            public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
            {
                var swt = service as IServiceWithType;
                if (swt == null
                    || swt.ServiceType == null
                    || !swt.ServiceType.IsAssignableTo<BaseHandler>())
                {
                    yield break;
                }

                yield return new ComponentRegistration(
                    Guid.NewGuid(),
                    new DelegateActivator(
                        swt.ServiceType,
                        (context, parameters) =>
                        {
                            var provider = context.Resolve<IHandlerFactory>();
                            var method = provider.GetType().GetMethod("GetHandler").MakeGenericMethod(swt.ServiceType);
                            return method.Invoke(provider, null);
                        }
                        ), 
                    new CurrentScopeLifetime(), 
                    InstanceSharing.None,
                    InstanceOwnership.OwnedByLifetimeScope,
                    new [] {service},
                    new ConcurrentDictionary<string, object>()
                    );
            }

            public bool IsAdapterForIndividualComponents => false;
        }

        public abstract class BaseHandler
        {
            public virtual string Handle(string message)
            {
                return "Handled: " + message;
            }
        }

        public class HandlerA : BaseHandler
        {
            public override string Handle(string message)
            {
                return "Handled by A: " + message;
            }
        }

        public class HandlerB : BaseHandler
        {
            public override string Handle(string message)
            {
                return "Handled by B: " + message;
            }
        }

        public interface IHandlerFactory
        {
            T GetHandler<T>() where T : BaseHandler;
        }

        public class HandlerFactory : IHandlerFactory
        {
            public T GetHandler<T>() where T : BaseHandler
            {
                return Activator.CreateInstance<T>();
            }
        }

        public class ConsumerA
        {
            HandlerA handlerA;

            public ConsumerA(HandlerA handlerA)
            {
                this.handlerA = handlerA ?? throw new ArgumentNullException();
            }

            public void DoWork()
            {
                Console.WriteLine(handlerA.Handle("Consumer A"));
            }
        }

        public class ConsumerB
        {
            HandlerB handlerB;

            public ConsumerB(HandlerB handlerB)
            {
                this.handlerB = handlerB ?? throw new ArgumentNullException();
            }

            public void DoWork()
            {
                Console.WriteLine(handlerB.Handle("Consumer B"));
            }
        }
    }
}
