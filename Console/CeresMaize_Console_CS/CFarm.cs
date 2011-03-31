using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CeresMaize_Console_CS
{

    // 农田类，记录农田中的作物和使用的GUI
    // CCropStateGUI为U3D的GUI，眼下只是模拟功能，以后将废弃
    public class CFarm
    {
        public CCrop crop = null;
        public CCropStateGUI stateGUI = null; // TODO:U3D
        public string farmName; // 农田的名称，用于显示在GameInfo和GUI中

        public bool isAssart = false;         // 因测试需要才设定public，在U3D中将测试
        public bool isSeminate = false;    // 因测试需要才设定public，在U3D中将测试
        public bool isReap = false;    // 因测试需要才设定public，在U3D中将测试
        // 修改下面的4个值会影响系统计算，在InitIrrigation和InitFertilizer中会自动调用
        public int soilWater = 40;
        public int soilN = 2;
        public int soilP = 1;
        public int soilK = 3;

        public CFarm(string name)
        {
            farmName = name;
        }

        public bool Assart()
        {
            if (isAssart)
            {
                CGameInfo.GetInstance().AddInfo(farmName + "无法开垦,因为农田已经开垦");
                return false;
            }
            if (!CCoin.GetInstance().processCoin(CCoinState.Assart))	//必须确保钱够才可以继续
                return false;
            isAssart = true;
            CGameInfo.GetInstance().AddInfo(farmName + "完成开垦");

            return true;
        }

        public bool Seminate(CSeed seed)
        {
            if (isSeminate)
            {
                CGameInfo.GetInstance().AddInfo(farmName + "无法播种,因为农田已经播种");
                return false;
            }

            if (isAssart)
            {
                if (!CCoin.GetInstance().processCoin(CCoinState.Seminate))	//必须确保钱够才可以继续
                    return false;
                CFarmFactroy.PlantCrop(this, seed);
                CGameInfo.GetInstance().AddInfo(farmName + "完成播种,种子是" + seed.des);
                isSeminate = true;
                isReap = true;	//设置农田为可收割状态
            }
            else
            {
                CGameInfo.GetInstance().AddInfo(farmName + "无法播种,因为农田没有开垦");
                return false;
            }

            return true;
        }

        public bool Reap()
        {
            if (!isAssart || !isSeminate)
            {
                CGameInfo.GetInstance().AddInfo(farmName + "无法收割,因为农田没有播种");
                return false;
            }
            else if (!isReap)
            {
                CGameInfo.GetInstance().AddInfo(farmName + "无法重复收割");
                return false;
            }
            else
            {
                crop = null;
                stateGUI = null;

                isAssart = false;
                isSeminate = false;
                isReap = false;

                CGameInfo.GetInstance().AddInfo(farmName + "完成收割");
                if (!CCoin.GetInstance().processCoin(CCoinState.Reap))	//处理获得的钱
                    return false;
            }

            return true;

        }

        public bool Irrigation(int water)
        {
            if (!CCoin.GetInstance().processCoin(CCoinState.Irrigation))	//必须确保钱够才可以继续
                return false;
            soilWater += water;

            if (crop is IExpandIrrigation)
            {
                ((IExpandIrrigation)crop).DoIrrigation(water);
            }

            CGameInfo.GetInstance().AddInfo(farmName + "完成灌溉,灌溉量是" + water + "方水");

            return true;
        }

        public bool Fertilizer(int n, int p, int k)
        {
            if (!CCoin.GetInstance().processCoin(CCoinState.Fertilizer))	//必须确保钱够才可以继续
                return false;
            soilN += n;
            soilP += p;
            soilK += k;

            if (crop is IExpandFertilizer)
            {
                ((IExpandFertilizer)crop).DoFertilizer(n, p, k);
            }

            CGameInfo.GetInstance().AddInfo(farmName + "完成施肥,有效施肥量是" + n + "公斤氮," + p + "公斤磷," + k + "公斤钾");

            return true;
        }

        public CSoilInfo GetSoilInfo()
        {
            return new CSoilInfo(soilWater, soilN, soilP, soilK);
        }

        public CCropState GetState()
        {
            // Farm State

            // Crop State
            if (crop != null)
            {
                return crop.GetState();
            }

            return null;
        }

        public void DailyUpdate()
        {
            if (crop != null)
            {
                crop.DailyUpdate();
            }
        }

    }


}
