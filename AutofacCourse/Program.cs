using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutofacCourse.S02;
using AutofacCourse.S02.L06;
using AutofacCourse.S02.L07;
using AutofacCourse.S03;

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
            

            Console.ReadKey();
        }
    }
}
