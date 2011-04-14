using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CeresMaize_Console_CS
{
    class Program
    {

        static void Main(string[] args)
        {
            CTerrain.GetInstance().Init("1.terrain");

            CFarm farm = new CFarm("农田");
            CFarmGUI gui = new CFarmGUI(farm);
            CMoney.GetInstance().Add(100);

            farm.Irrigation();
            farm.Fertilizer(CFertilizer.Fertilizer_Com);
            farm.Assart();
            farm.Seminate(new CSeed(CSeedType.Maize_Normal));

            farm.KillWeed();
            farm.inWeed = true;
            farm.inPoor = true;

            farm.DailyUpdate();
            farm.KillWeed();
                        
            farm.stateGUI.Show(farm.crop.Predict());

            farm.Irrigation();
            for (int i = 0; i <200; i++)
            {
                farm.DailyUpdate();
                CTerrain.GetInstance().dt = CTerrain.GetInstance().dt.AddDays(1);
                //farm.stateGUI.Show(farm.GetCropState());
                if (i%100 == 0)
                    continue;
            }
            farm.Reap();

            for (int i = 0; i < CGameInfo.GetInstance().GetInfoLength(); i++)
            {
                System.Console.Out.WriteLine(CGameInfo.GetInstance().GetInfoAt(i));
            }
            /*
            ClassContain oth = new ClassContain(new ClassB());
            oth.Test();
            oth.SetInter(new ClassA());
            oth.Test();
            */
            System.Console.Out.WriteLine(CMoney.GetInstance().GetMoney());
            
            System.Console.Read();
        }
    }
}
