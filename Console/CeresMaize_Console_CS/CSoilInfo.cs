using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace CeresMaize_Console_CS
{
    public class CSoilInfo
    {
        public float Water;               //土壤含水量
        public float N;                   //土壤含氮量
        public float P;                   //土壤含磷量
        public float K;                   //土壤含钾量

        //private float cropFactor;        //作物系数
        public int cropQualityLimit;    //农作物质量下限
        public int cropNumberLimit;     //农作物数量下限

        private float[] punishmentRecord = new float[5];        //记录惩罚措施里累计的时间

        private float[,] punishmentInfo = new float[5, 8];     //惩罚的相关信息
        private int[,] playInfoSet = new int[2, 4];            //需要将土壤成分设置为***的相关信息
        private float[,] playInfoChange = new float[6, 4];     //操作改变土壤的相关信息

        public CSoilInfo(int w,int n,int p,int k)
        {
            Water = w;
            N = n;
            P = p;
            K = k;
        }

        //无参数的构造函数
        public CSoilInfo()
        {
            readSoilInfo();
        }

        //从文件读取土壤信息
        public void readSoilInfo()
        {
            int i, j;
            string[] lines;     //保存每行的字符串
            string[] sp;        //保存行拆开后的字符串
            //
            //从文件获取惩罚相关数据
            //
            lines = File.ReadAllLines("punishmentInfo.txt");
            for (i = 0; i < 5; i++)
            {
                sp = lines[i + 1].Split('\t');
                for (j = 0; j < 8; j++)
                {
                    punishmentInfo[i, j] = Convert.ToSingle(sp[j + 1]);
                }
            }

            //
            //从文件获取重置信息相关数据
            //
            lines = File.ReadAllLines("playInfoSet.txt");
            for (i = 0; i < 2; i++)
            {
                sp = lines[i + 1].Split('\t');
                for (j = 0; j < 4; j++)
                {
                    playInfoSet[i, j] = Convert.ToInt32(sp[j + 1]);
                }
            }

            //
            //从文件操作改变土壤相关数据
            //
            lines = File.ReadAllLines("playInfoChange.txt");
            for (i = 0; i < 6; i++)
            {
                sp = lines[i + 1].Split('\t');
                for (j = 0; j < 4; j++)
                {
                    playInfoChange[i, j] = Convert.ToSingle(sp[j + 1]);
                }
            }
        }

        /// <summary>
        /// 外界因素每日对土壤的影响
        /// </summary>
        /// <param name="farm">被影响的农田</param>
        public void DailyUpdateEffect(CFarm farm)
        {
            //
            //温度和光照造成的影响，这部分参数写死在程序里
            //


            //
            //每日对土地固定的影响
            //
            changePlayInfo(4);
            if (farm.crop != null)   //如果农场的作物不为空，则作物也消耗
                changePlayInfo(5);
        }

        /// <summary>
        /// 如果土地没满足条件，则执行每日惩罚
        /// </summary>
        /// <param name="farm">该农田的农作物被惩罚</param>
        public void DailyUpdatePunishment(CFarm farm)
        {
            int i;


            for (i = 0; i < 5; i++)     //处罚累计时间自加
                punishmentRecord[i]++;
        }

        /// <summary>
        /// 利用playInfoChange的数据改变 水、N、P、K的数量
        /// </summary>
        /// <param name="row">要利用playInfoChange的第row行的数据</param>
        public void changePlayInfo(int row)
        {
            Water += playInfoChange[row, 0];
            N += playInfoChange[row, 1];
            P += playInfoChange[row, 2];
            K += playInfoChange[row, 3];

            //如果变化后小于0，则归0
            if (Water < 0)
                Water = 0;
            if (N < 0)
                N = 0;
            if (P < 0)
                P = 0;
            if (K < 0)
                K = 0;
        }
    }


    //各种操作的状态，用于处理对土地的影响

}

