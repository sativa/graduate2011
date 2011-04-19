using System;
using System.Collections.Generic;
using System.Text;

namespace CeresMaize_Console_CS
{
    class Program
    {

        static void Main(string[] args)
        {
            CTerrain.GetInstance().Init("1.terrain");

            CFarm farm = new CFarm("农田1");
            //farm.lastCrop = new CSeed(ESeedType.Maize_Normal);
            CFarmGUI gui = new CFarmGUI(farm);

            CGameInfo.GetInstance().AddInfo("初始的金钱:"+CCoin.GetInstance().getCoin());

            farm.Assart();
            CGameInfo.GetInstance().AddInfo("开垦后金钱:" + CCoin.GetInstance().getCoin());

            farm.Irrigation();
            CGameInfo.GetInstance().AddInfo("灌溉后金钱:" + CCoin.GetInstance().getCoin());

            farm.Fertilizer(EFertilizerType.Fertilizer_Com);
            CGameInfo.GetInstance().AddInfo("施肥后金钱:" + CCoin.GetInstance().getCoin());

            farm.Seminate(new CSeed(ESeedType.Maize_Normal));
            CGameInfo.GetInstance().AddInfo("播种后金钱:" + CCoin.GetInstance().getCoin());

            farm.HandleWeed();
            CGameInfo.GetInstance().AddInfo("除草后金钱:" + CCoin.GetInstance().getCoin());

            /*
            farm.inWeed = true;
            farm.inPoor = true;
            farm.inDry = true;
            farm.inPet = true;
            */

            //farm.Reap();
            CGameInfo.GetInstance().AddInfo("收割后金钱:" + CCoin.GetInstance().getCoin());


            farm.DailyUpdate();
            CGameInfo.GetInstance().AddInfo("土壤:"+farm.GetSoilInfo().ToString());

            farm.HandleWeed();
            CGameInfo.GetInstance().AddInfo("除草后金钱:" + CCoin.GetInstance().getCoin());

            farm.HandlePet();
            CGameInfo.GetInstance().AddInfo("土壤:" + farm.GetSoilInfo().ToString());
            CGameInfo.GetInstance().AddInfo("除虫后金钱:" + CCoin.GetInstance().getCoin());

            CGameInfo.GetInstance().AddInfo("预测结果:" + farm.Predict());

            farm.HandleWeed();
            CGameInfo.GetInstance().AddInfo("除草后金钱:" + CCoin.GetInstance().getCoin());

            // 模拟生长
            for (int i = 0; i <200; i++)
            {
                farm.DailyUpdate();
                CTerrain.GetInstance().dt = CTerrain.GetInstance().dt.AddDays(1);
                CGameInfo.GetInstance().AddInfo("土壤:"+farm.GetSoilInfo());
                //CGameInfo.GetInstance().AddInfo("作物:"+farm.GetCropState());
                if (i == 100)
                    continue;
            }
            CGameInfo.GetInstance().AddInfo("预测结果:" + farm.Predict());

            farm.Reap();
            CGameInfo.GetInstance().AddInfo("收割后金钱:" + CCoin.GetInstance().getCoin());
            
            CGameInfo.GetInstance().AddInfo("最终的金钱:" + CCoin.GetInstance().getCoin());



            // 输出所有结果
            for (int i = 0; i < CGameInfo.GetInstance().GetInfoLength(); i++)
            {
                Console.Out.WriteLine(CGameInfo.GetInstance().GetInfoAt(i));
            }
            System.Console.Read();
        }
    }
}
