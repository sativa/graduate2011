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
            CFarm farm = new CFarm("农田");
            CFarmGUI gui = new CFarmGUI(farm);
            DateTime dt = new DateTime(2011, 1, 1);
            CMoney.GetInstance().Add(100);

            gui.ButtonAssart();
            gui.ButtonSeminate(new CSeed("MaizeSeed"));

            //gui.ButtonIrrigation(40);

            //gui.ButtonFertilizer(2, 1, 3);
            farm.stateGUI.Show(farm.crop.Predict());

            gui.ButtonIrrigation(10);
            for (int i = 0; i <200; i++)
            {
                farm.DailyUpdate();
                dt = dt.AddDays(1);
                farm.stateGUI.Show(farm.GetState());
                if (i%100 == 0)
                    continue;
            }
            gui.ButtonReap();


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
