﻿using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

    public class CSoilInfo
    {
        public CFarm farm;  //  农田的引用

        public float Water;               //土壤含水量
        public float N;                   //土壤含氮量
        public float P;                   //土壤含磷量
        public float K;                   //土壤含钾量

        //private float cropFactor;        //作物系数
        private float[] punishmentRecord = new float[5];        //记录惩罚措施里累计的时间
        private float[,] SoilInfoPunish = new float[5, 8];     //惩罚的相关信息
        private int[,] SoilInfoInit = new int[2, 4];            //需要将土壤成分设置为***的相关信息
        private float[,] SoilInfoChange = new float[9, 4];     //操作改变土壤的相关信息

        //无参数的构造函数
        public CSoilInfo(CFarm thisFarm)
        {
            farm = thisFarm;

            ReadSoilInfo();
        }

        public void DailyUpdate()
        {
            DailyUpdateEffect();
            DailyUpdatePunishment();
        }

        /// <summary>
        /// 环境因素对土壤的影响
        /// </summary>
        private void DailyUpdateEffect()
        {
            // 每日土壤损失
            Water -= CTerrain.GetInstance().SOLRAD[Control_Time.dt.DayOfYear - 1] / 200;

            if (CTerrain.GetInstance().TEMPMN[Control_Time.dt.DayOfYear - 1] + CTerrain.GetInstance().TEMPMX[Control_Time.dt.DayOfYear - 1] > 20)
                Water -= (CTerrain.GetInstance().TEMPMN[Control_Time.dt.DayOfYear - 1] + CTerrain.GetInstance().TEMPMX[Control_Time.dt.DayOfYear - 1] - 20) / 10;
            //  每日土壤损失
            ChangeSoilInfo(4);

            //  每日土壤损失，种植作物
            if (farm.crop != null)
                ChangeSoilInfo(5);
        }

        /// <summary>
        /// 如果土地满足条件，则执行每日状态惩罚
        /// </summary>
        /// <param name="farm">该农田的农作物被惩罚</param>
        private void DailyUpdatePunishment()
        {
            // 干涸
            if (farm.inDry)
            {
                if (farm.crop != null)
                {
                    farm.crop.cropQuality -= SoilInfoPunish[0, 4];
                    //farm.crop.cropNumber -= SoilInfoPunish[0, 5];
                }
            }

            // 虫害
            if (farm.inPet)
            {
                 if (farm.crop != null)
                {
                    farm.crop.cropQuality -= SoilInfoPunish[1, 4];
                    //farm.crop.cropNumber -= SoilInfoPunish[1, 5];
                }
            }

            // 草害
            if (farm.inWeed)
            {
                if (farm.crop != null)
                {
                    farm.crop.cropQuality -= SoilInfoPunish[2, 4];
                    //farm.crop.cropNumber -= SoilInfoPunish[2, 5];
                }
            }
            
            // 贫瘠处理
            if (farm.inPoor) // 处罚累计时间自加
                punishmentRecord[3]++;
            else
                punishmentRecord[3]=0;
            if (punishmentRecord[3] >= SoilInfoPunish[3, 6])  // 累计X天后
            {
                if (farm.crop != null)
                {
                    farm.crop.cropQuality -= SoilInfoPunish[3, 4];
                    //farm.crop.cropNumber -= SoilInfoPunish[3, 5];
                }
            }

            // 成熟
            if (farm.isReap)
            {
                if (farm.crop != null)
                {
                    farm.crop.cropQuality -= SoilInfoPunish[4, 4];
                    //farm.crop.cropNumber -= SoilInfoPunish[4, 5];
                }
            }

            if (farm.crop != null )
                if(farm.crop.cropQuality < 0)
                 farm.crop.cropQuality = 0;

        }

        /// <summary>
        /// 利用SoilInfoInit的数据改变 水、N、P、K的数量
        /// </summary>
        /// <param name="row">要利用SoilInfoInit的第row行的数据</param>
        public void InitSoliInfo(int row)
        {
            if (SoilInfoInit[row, 0]!= -1)
                Water = SoilInfoInit[row, 0];

            N = SoilInfoInit[row, 1];
            P = SoilInfoInit[row, 2];
            K = SoilInfoInit[row, 3];
        }

        /// <summary>
        /// 利用SoilInfoChange的数据改变 水、N、P、K的数量
        /// </summary>
        /// <param name="row">要利用SoilInfoChange的第row行的数据</param>
        public void ChangeSoilInfo(int row)
        {
            Water += SoilInfoChange[row, 0];
            N += SoilInfoChange[row, 1];
            P += SoilInfoChange[row, 2];
            K += SoilInfoChange[row, 3];

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

        public override string ToString()
        {
            return "水分:" + Water.ToString()
                + " N:" + N.ToString() + ","
                + " P:" + P.ToString() + ","
                + " K:" + K.ToString();
        }


        //从文件读取土壤信息
        private void ReadSoilInfo()
        {
            int i, j;
            string[] lines;     //保存每行的字符串
            string[] sp;        //保存行拆开后的字符串

            //
            //从文件获取惩罚相关数据
            //
            lines = File.ReadAllLines("SoilInfoPunish.txt");
            for (i = 0; i < 5; i++)
            {
                sp = lines[i + 1].Split('\t');
                for (j = 0; j < 8; j++)
                {
                    SoilInfoPunish[i, j] = Convert.ToSingle(sp[j + 1]);
                }
            }

            //
            //从文件获取重置信息相关数据
            //
            lines = File.ReadAllLines("SoilInfoInit.txt");
            for (i = 0; i < 2; i++)
            {
                sp = lines[i + 1].Split('\t');
                for (j = 0; j < 4; j++)
                {
                    SoilInfoInit[i, j] = Convert.ToInt32(sp[j + 1]);
                }
            }

            //
            //从文件操作改变土壤相关数据
            //
            lines = File.ReadAllLines("SoilInfoChange.txt");
            for (i = 0; i < 9; i++)
            {
                sp = lines[i + 1].Split('\t');
                for (j = 0; j < 4; j++)
                {
                    SoilInfoChange[i, j] = Convert.ToSingle(sp[j + 1]);
                }
            }
        }
    }


