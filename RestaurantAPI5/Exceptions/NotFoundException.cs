using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI5.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) // base constructor from Exception class
        {

        }
    }
}
