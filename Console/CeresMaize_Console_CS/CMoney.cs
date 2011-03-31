using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CeresMaize_Console_CS
{
    public class CMoney
    {
        static private CMoney instance = new CMoney();

        private int value;

        static public CMoney GetInstance()
        {
            return instance;
        }
        
        public bool Add(int i)
        {
            if (value + i < 0)
                return false;

            value += i;
            return true;
        }

        public int GetMoney()
        {
            return value;
        }
    }
}
