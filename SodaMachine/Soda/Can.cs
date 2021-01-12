using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    abstract class Can
    {
        //Member Variables (Has A)
        protected double price;
        protected string name;

        public double Price
        { 
            get 
            {
                return price;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
        //Constructor (Spawner)

        //Member Methods (Can Do)
    }
}
