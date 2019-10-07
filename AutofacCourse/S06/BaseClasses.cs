using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacCourse.S06
{
    public interface IVehicle
    {
        void Go();
    }

    public class Truck : IVehicle
    {
        private IDriver driver;

        public Truck(IDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public void Go()
        {
            driver.Drive();
        }
    }

    public interface IDriver
    {
        void Drive();
    }

    public class SaneDriver : IDriver
    {
        public void Drive()
        {
            Console.WriteLine("Driving safely.");
        }
    }

    public class CrazyDriver : IDriver
    {
        public void Drive()
        {
            Console.WriteLine("Going to fast!");
        }
    }
}
