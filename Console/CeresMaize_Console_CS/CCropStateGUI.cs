using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CeresMaize_Console_CS
{
    // 作物状态GUI，显示作物的状态信息
    // 为U3D的GUI，眼下只是模拟功能，以后将废弃
    abstract public class CCropStateGUI
    {
        public CFarm farm;

        public CCropStateGUI(CFarm thisFarm)
        {
            farm = thisFarm;
        }

        abstract public void Show(CCropState state);
    }

    public class CMaizeStateGUI : CCropStateGUI
    {        

        public CMaizeStateGUI(CFarm farm)
            : base(farm)
        {

        }

        override public void Show(CCropState state)
        {
            System.Console.WriteLine(state);
        }
    }
}
