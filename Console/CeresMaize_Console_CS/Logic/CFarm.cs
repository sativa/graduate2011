using System;
using System.Collections.Generic;
using System.Text;



    // 农田类，记录农田中的作物和使用的GUI
    // CCropStateGUI为U3D的GUI，眼下只是模拟功能，以后将废弃
    public class CFarm
    {
        public Mod_Farm farmMod=null;
        public CCrop crop = null;            // 当前种植的作物
        public CSeed lastCrop = null;      // 上次种植的作物，用于进行播种惩罚

        public CCropStateGUI stateGUI = null; // TODO:U3D
        public string farmName; // 农田的名称，用于显示在GameInfo和GUI中

        public bool isAssart = false;         // 因测试需要才设定public，在U3D中将测试
        public bool isSeminate = false;    // 因测试需要才设定public，在U3D中将测试
        public bool isReap = false;          // 因测试需要才设定public，在U3D中将测试
        public bool inWeed = false;    // 杂草 // 因测试需要才设定public，在U3D中将测试
        public bool inPet = false;        // 虫害  // 因测试需要才设定public，在U3D中将测试
        public bool inPoor = false;      // 贫瘠   // 因测试需要才设定public，在U3D中将测试
        public bool inDry = false;       // 干涸   // 因测试需要才设定public，在U3D中将测试
        public bool isDead = false;     // 死亡

        public CSoilInfo soilInfo;     // 土壤 

        public CFarm(string name,Mod_Farm mod)
        {
            farmName = name;
            farmMod = mod;

            soilInfo = new CSoilInfo(this);
            soilInfo.InitSoliInfo(0);
        }

        public bool Assart()
        {
            if (isAssart)
            {
                CGameInfo.GetInstance().AddInfo(farmName + "无法开垦,因为农田已经开垦");
                return false;
            }
            if (!CCoin.GetInstance().processCoin(ECoinState.Assart))	//必须确保钱够才可以继续
                return false;


            // 土壤数据的初始化
            soilInfo.InitSoliInfo(1);  

            CGameInfo.GetInstance().AddInfo(farmName + "完成开垦");
            isAssart = true;

            // 改变外观
            farmMod.Switch(1);

            return true;
        }

        public bool Seminate(CSeed seed)
        {
            if (isSeminate)
            {
                CGameInfo.GetInstance().AddInfo(farmName + "无法播种,因为农田已经播种");
                return false;
            }

            if (!isAssart)
            {
                CGameInfo.GetInstance().AddInfo(farmName + "无法播种,因为农田没有开垦");
                return false;
            }

            if (!CCoin.GetInstance().processCoin(ECoinState.Seminate))	 //必须确保钱够才可以继续
                return false;

            CFarmFactroy.PlantCrop(this, seed);
            CGameInfo.GetInstance().AddInfo(farmName + "完成播种,种子是" + seed.des);

            // 播种惩罚
            if (Control_Time.season != seed.season)
            {
                crop.cropQuality -= 20;
            }
            if (lastCrop != null && lastCrop.type == seed.type)
            {
                crop.cropQuality -= 20;
            }

            lastCrop = seed;    // 标记上次种植的作物
            isSeminate = true;
            return true;
        }

        /// <summary>
        /// 农田是否收割，由作物成熟决定
        /// </summary>
        /// <returns>能否收割</returns>
        private bool IsReap()
        {
            if (crop == null)
                return false;
            return crop.IsReap();
        }

        /// <summary>
        /// 农田收割操作
        /// </summary>
        /// <returns>能否执行</returns>
        public bool Reap()
        {
            

            if (!isAssart || !isSeminate)
            {
                CGameInfo.GetInstance().AddInfo(farmName + "无法收割,因为农田没有播种");
                return false;
            }
            else if (!isReap)
            {
                CGameInfo.GetInstance().AddInfo(farmName + "无法收割,因为作物没有成熟");
                return false;
            }
            else
            {
                crop = null;
                stateGUI = null;

                isAssart = false;
                isSeminate = false;
                isReap = false;

                // TODO:此时虫害是否消失？
                inPet = false;

                CGameInfo.GetInstance().AddInfo(farmName + "完成收割");
                if (!CCoin.GetInstance().processCoin(ECoinState.Reap))	//处理获得的钱
                    return false;
            }

            farmMod.Switch(0);

            return true;

        }

        public bool Irrigation()
        {
            if (!CCoin.GetInstance().processCoin(ECoinState.Irrigation))	//必须确保钱够才可以继续
                return false;

            float water= soilInfo.Water;

            // 土壤数据修改
            soilInfo.ChangeSoilInfo(0);

            if (crop is IExpandIrrigation)
            {
                ((IExpandIrrigation)crop).DoIrrigation(soilInfo.Water - water);
            }

            CGameInfo.GetInstance().AddInfo(farmName + "完成灌溉,灌溉量是" + (soilInfo.Water-water) + "方水");

            if (inDry && soilInfo.Water > 20)
            {
                inDry = false;
                CGameInfo.GetInstance().AddInfo(farmName + "因灌溉已不再干旱");
            }

            return true;
        }

        public bool Fertilizer(EFertilizerType fertilizer)
        {
            if (!CCoin.GetInstance().processCoin(ECoinState.Fertilizer))	//必须确保钱够才可以继续
                return false;

            //soilInfo.N += n;
            //soilInfo.P += p;
            //soilInfo.K += k;

            // 保存施肥前的数据
            float n, p, k;
            n = soilInfo.N;
            p = soilInfo.P;
            k = soilInfo.K;

            // 土壤数据修改
            switch (fertilizer)
            {
                case EFertilizerType.Fertilizer_N:
                    soilInfo.ChangeSoilInfo(6);
                    break;
                case EFertilizerType.Fertilizer_P:
                    soilInfo.ChangeSoilInfo(7);
                    break;
                case EFertilizerType.Fertilizer_K:
                    soilInfo.ChangeSoilInfo(8);
                    break;
                case EFertilizerType.Fertilizer_Com:
                    soilInfo.ChangeSoilInfo(3);
                    break;
            }

            if (crop is IExpandFertilizer)
            {
                ((IExpandFertilizer)crop).DoFertilizer(soilInfo.N - n, soilInfo.P - p, soilInfo.K - k);
            }

            CGameInfo.GetInstance().AddInfo(farmName + "完成施肥,有效施肥量是" + (soilInfo.N - n) + "公斤氮," + (soilInfo.P - p) + "公斤磷," + (soilInfo.K - k) + "公斤钾");

            if (soilInfo.N > 0 && soilInfo.P > 0 && soilInfo.K > 0)
            {
                inPoor = false;
                CGameInfo.GetInstance().AddInfo(farmName + "因施肥已不再贫瘠");
            }

            return true;
        }

        /// <summary>
        /// 除草操作
        /// </summary>
        /// <returns>是否成功</returns>
        public bool HandleWeed()
        {
            if (!inWeed)
            {
                CGameInfo.GetInstance().AddInfo(farmName + "没有发生草害，不用除草");
                return false;
            }

            if (!CCoin.GetInstance().processCoin(ECoinState.Weed))	//必须确保钱够才可以继续
                return false;

            // 土壤数据修改
            soilInfo.ChangeSoilInfo(2);

            CGameInfo.GetInstance().AddInfo(farmName + "完成除草操作");
            inWeed = false;

            return true;
        }

        /// <summary>
        /// 除虫操作
        /// </summary>
        /// <returns>是否成功</returns>
        public bool HandlePet()
        {
            if (!inPet)
            {
                CGameInfo.GetInstance().AddInfo(farmName + "没有发生虫害，不用除虫");
                return false;
            }

            if (!CCoin.GetInstance().processCoin(ECoinState.Pet))	//必须确保钱够才可以继续
                return false;

            // 土壤数据修改
            soilInfo.ChangeSoilInfo(1);

            CGameInfo.GetInstance().AddInfo(farmName + "完成除虫操作");
            inPet = false;

            return true;
        }



        /// <summary>
        ///  预测操作
        /// </summary>
        /// <returns>是否成功</returns>
        public CCropState Predict()
        {
            if (crop == null)
            {
                CGameInfo.GetInstance().AddInfo(farmName+"上没有种植任何作物");
                return null;
            }

            CCropState result= crop.Predict();
            if (result== null)
                return null;

            return result;
        }

        /// <summary>
        /// 依据条件刷新农场状态，如杂草、虫害、干涸等爆发
        /// </summary>
        public void DailyUpdateFarm()
        {
            // 查看作物是否死亡
            if (crop != null && crop.cropQuality == 0)
            {
                isDead = true;
            }

            // 判定是否干涸
            if (!inDry)
            {
                if (soilInfo.Water < 20)
                {
                    inDry = true;
                    CGameInfo.GetInstance().AddInfo(farmName + "因缺水已处于干涸状态");
                }
            }

            Random random = new Random();
            int value;
            // 判定是否虫害
            if (!inPet)
            {
                value = random.Next(0, 100);
                if (value > 80)
                {
                    inPet = true;
                    CGameInfo.GetInstance().AddInfo(farmName + "发生了虫害,请尽快处理");
                }
            }

            // 判定是否草害
            if (!inWeed)
            {
                value = random.Next(0, 100);
                if (value < 20)
                {
                    inWeed = true;
                    CGameInfo.GetInstance().AddInfo(farmName + "发生了草害,请尽快处理");
                }
            }

            // 判定是否贫瘠
            if (!inPoor)
            {
                if (soilInfo.N == 0 || soilInfo.P == 0 || soilInfo.K == 0)
                {
                    inPoor = true;
                    CGameInfo.GetInstance().AddInfo(farmName + "因缺养分已处于贫瘠状态");
                }
            }


            // 查看作物是否成熟
            isReap = IsReap();

        }

        public CSoilInfo GetSoilInfo()
        {
            return soilInfo;
        }

        public CCropState GetCropState()
        {
            // Crop State
            if (crop != null)
            {
                return crop.GetState();
            }

            return null;
        }

        public void DailyUpdate()
        {
            DailyUpdateFarm();  // 农田刷新自己

            if (crop != null)
            {
                crop.DailyUpdate();
            }

            if (soilInfo != null)
            {
                soilInfo.DailyUpdate();
            }
        }
    }

