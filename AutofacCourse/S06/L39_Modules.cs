using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Module = Autofac.Module;

namespace AutofacCourse.S06
{
    public class TransportModule : Module
    {
        public bool ObeySpeedLimit { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            if (ObeySpeedLimit)
            {
                builder.RegisterType<SaneDriver>().As<IDriver>();
            }
            else
            {
                builder.RegisterType<CrazyDriver>().As<IDriver>();
            }

            builder.RegisterType<Truck>().As<IVehicle>();
        }
    }

    class L39_Modules
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();
            builder.RegisterModule(new TransportModule {ObeySpeedLimit = true}); // konfigurowanie modulu
            using (var c = builder.Build())
            {
                c.Resolve<IVehicle>().Go();
            }
        }
    }
}
