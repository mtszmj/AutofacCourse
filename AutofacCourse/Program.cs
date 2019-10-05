using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutofacCourse.S02;
using AutofacCourse.S02.L06;
using AutofacCourse.S02.L07;
using AutofacCourse.S03;
using AutofacCourse.S04;
using AutofacCourse.S05;

namespace AutofacCourse
{
    class Program
    {
        static void Main(string[] args)
        {
            // Section 2
            L06_WithoutDI.Run();
            L07_RegisteringTypes.Run();
            L08_DefaultRegistrations.Run();
            L09_ChoiceOfConstructors.Run();
            L10_RegisteringInstances.Run();
            L11_Lambda.Run();
            L12_Generics.Run();

            // Section 3
            L15_PassingParametersToRegister.Run();
            L16_PropertyAndMethodInjection.Run();
            L17_ScanningForTypes.Run();
            L18_ScanningForModules.Run();
            
            // Section 4
            L21_DelayedInstantiation.Run();
            L22_ControlledInstantiation.Run();
            L23_DynamicInst.Run();
            L24_ParametrizedInst.Run();
            L25_Enumeration.Run();
            L26_MetadataInterrogation.Run();
            L27_KeyedServiceLookup.Run();
            L28_ContainerIndependence.Run();

            // Section 5
            L31_InstanceScope.Run();
            L32_CaptiveDependencies.Run();
            L33_Disposal.Run();
            L34_LifetimeEvents.Run();
            L35_Startup.Run();


            Console.ReadKey();
        }
    }
}
