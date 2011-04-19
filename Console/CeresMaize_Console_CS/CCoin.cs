using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace CeresMaize_Console_CS
{


    //singlton
    public class CCoin
    {

        static private CCoin instance = new CCoin();
        private int coin;				//金币数量
        public int[] seedBuyPrice = new int[100];	//种子买入价格
        public int[] seedSalePrice = new int[100];	//农作物卖出价格
        public int[] farmOperation = new int[20];	//农场其他操作价格

        static public CCoin GetInstance()
        {
            return instance;
        }

        //构造函数
        private CCoin()
        {
            readCoinInfo();
        }

        //处理钱的所有操作
        public bool processCoin(ECoinState state)
        {
            if (state == ECoinState.None)
                return false;
            else if (processSeed(state))				//种子
                return true;
            else if (processFarmOperation(state))	//耕地等农场操作
                return true;
            else if (processSale(state))				//收获
                return true;
            else if (processUpdateFarm(state))		//升级土地
                return true;
            else		//没钱的处理
            {
                CGameInfo.GetInstance().AddInfo("太穷了吧，这点钱都没有");
                return false;
            }
        }

        //买种子
        bool processSeed(ECoinState state)
        {
            //暂时只有玉米，以后复杂了和商店联系起来
            switch (state)
            {
                case ECoinState.Seminate:
                    return addOrMinusMoney(seedBuyPrice[0], false);
                default:
                    return false;
            }
        }

        //耕地除草等操作花费金币
        bool processFarmOperation(ECoinState state)
        {
            switch (state)
            {
                case ECoinState.Assart:
                    return addOrMinusMoney(farmOperation[0], false);
                case ECoinState.Irrigation:
                    return addOrMinusMoney(farmOperation[1], false);
                case ECoinState.Fertilizer:
                    return addOrMinusMoney(farmOperation[2], false);
                case ECoinState.Weed:
                    return addOrMinusMoney(farmOperation[3], false);
                case ECoinState.Pet:
                    return addOrMinusMoney(farmOperation[4], false);
                default:
                    return false;
            }
        }

        //土地升级
        bool processUpdateFarm(ECoinState state)
        {
            return false;
        }

        //收获时金币操作
        bool processSale(ECoinState state)
        {
            //暂时只有玉米
            switch (state)
            {
                case ECoinState.Reap:
                    return addOrMinusMoney(seedSalePrice[0], true);
                default:
                    return false;
            }
        }

        //扣钱和加钱操作，参数money为钱数，add为加钱标志true为加钱false为扣钱
        bool addOrMinusMoney(int money, bool add)
        {
            if (add)
                coin += money;
            else
            {
                if (coin - money >= 0)	//确保钱不会为负数
                    coin -= money;
                else
                    return false;
            }
            return true;
        }

        //读取金币信息文件，现在没文件先写死在这里面
        void readCoinInfo()
        {
            coin = 100;		//金币初始化为100
            //种子购买价格
            seedBuyPrice[0] = 9;
            //卖出价格
            seedSalePrice[0] = 70;
            //耕地除草等操作价格
            farmOperation[0] = 1;	//开垦
            farmOperation[1] = 1;	//灌溉
            farmOperation[2] = 1;	//施肥
            farmOperation[3] = 1;	//除草
            farmOperation[4] = 1;	//除虫（小）
            farmOperation[5] = 2;	//除虫（中）
            farmOperation[6] = 3;	//除虫（大）
            //土地升级价格（待补充）
        }

        //返回金币值
        public int getCoin()
        {
            return coin;
        }
    }

    
};