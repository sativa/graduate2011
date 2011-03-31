using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CeresMaize_Console_CS
{
    // 农田GUI，绑定在农田模型上
    // 为U3D的GUI，眼下只是模拟功能，以后将废弃
    class CFarmGUI
    {
        CFarm farm;

        public CFarmGUI(CFarm thisFarm)
        {
            farm = thisFarm;
        }
        
        public void ButtonAssart()
        {
            farm.Assart();
        }

        public void ButtonSeminate(CSeed seed)
        {
            farm.Seminate(seed);
        }

        public void ButtonReap()
        {
            farm.Reap();
        }

        public void ButtonIrrigation(int water)
        {
            farm.Irrigation(water);
        }

        public void ButtonFertilizer(int n,int p,int k)
        {
            farm.Fertilizer(n,p,k);
        }
    }

}
