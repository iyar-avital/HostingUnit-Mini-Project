using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    enum TypeError
    {
        Add, Update
    }

    class addOrderException : Exception
    {
        public readonly Type type;
        public addOrderException(string mes, Type t) : base(mes)
        {
            type = t;
        }
    }
}
