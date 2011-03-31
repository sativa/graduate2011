using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CeresMaize_Console_CS
{
    class ClassA:Interface
    {
        #region Interface 成员

        public string Test()
        {
            return "ClassA";
        }

        #endregion
    }

    class ClassB: Interface
    {
        #region Interface 成员

        public string Test()
        {
            return "ClassB";
        }

        #endregion
    }
    class ClassContain
    {        
        Interface i;
        public ClassContain(Interface iter)
        {
           i=iter;
        }

        public void SetInter(Interface newI)
        {
            i=newI;
        }

        public void Test()
        {
            System.Console.WriteLine(i.Test());
        }
    }
}
