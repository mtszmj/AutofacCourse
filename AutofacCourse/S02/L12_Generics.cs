using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AutofacCourse.S02
{
    public static class L12_Generics
    {
        public static void Run()
        {
            Console.WriteLine($"\n----- {MethodBase.GetCurrentMethod().DeclaringType.Name} -----");

            var builder = new ContainerBuilder();

            // jak ktos bedzie chcial IList<T> to dostanie List<T>
            // IList<int> -> List<int>
            builder.RegisterGeneric(typeof(List<>)).As(typeof(IList<>));

            IContainer container = builder.Build();
            var myList = container.Resolve<IList<int>>();
            Console.WriteLine(myList.GetType());
        }
    }
}
