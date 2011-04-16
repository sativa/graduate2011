using System;
using System.Collections.Generic;
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

        public void ButtonIrrigation()
        {
            farm.Irrigation();
        }

        public void ButtonFertilizer(CFertilizer fer)
        {
            farm.Fertilizer(fer);
        }
    }

}
