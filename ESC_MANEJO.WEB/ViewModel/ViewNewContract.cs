using ESC_MANEJO.CORE.Entities;
using ESC_MANEJO.CORE.Entities.Administrator;
using System.Collections.Generic;

namespace ESC_MANEJO.WEB.ViewModel
{
    public class ViewNewContract
    {
        public List<Vehicle> Vehicles { get; set; }
        public List<User> Drivers { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
