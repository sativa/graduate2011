using System;
using System.Collections.Generic;
using System.Text;


    // 玉米科学模型，实现了CCrop操作
    public class CeresMaize_Logic : CCrop, IExpandFertilizer, IExpandIrrigation
    {
        //public string cropName = "玉米";
        private float coeIrrigation = 1;     // coeffient Water
        private float coeFertilizer = 1; // coeffient Fertilizer

        #region ExpandFertilizer 接口成员
        private float curFertilizer_N = 0;   // current Fertilizer add by User Action : Fertilize
        private float curFertilizer_P = 0;
        private float curFertilizer_K = 0;

        private float optFertilizer_N = 0;   // optimization Fertilizer Different with Maize ISTAGE 
        private float optFertilizer_P = 0;
        private float optFertilizer_K = 0;

        private float offsetFertilizer_N = 0;  // offset Fertilizer
        private float offsetFertilizer_P = 0;
        private float offsetFertilizer_K = 0;

        private float totalCurFertilizer_N = 0;  //sum of curFertilizer
        private float totalCurFertilizer_P = 0;
        private float totalCurFertilizer_K = 0;

        private float totalOptFertilizer_N = 0;  //sum of optFertilizer
        private float totalOptFertilizer_P = 0;
        private float totalOptFertilizer_K = 0;

        public float CalFertilizer()
        {
            //getOptFertilizerandOffsetFertilizer();   // 依据生长阶段设置最佳施肥量，是否需要？？
            return CheckFertilizer();
        }

        public void DoFertilizer(float n, float p, float k)
        {
            curFertilizer_N += n;
            curFertilizer_P += p;
            curFertilizer_K += k;

            totalCurFertilizer_N += n;
            totalCurFertilizer_P += p;
            totalCurFertilizer_K += k;
        }

        // call by Construct Auto
        public void InitFertilizer()
        {
            DoFertilizer(farm.soilInfo.N, farm.soilInfo.P, farm.soilInfo.K);
            totalOptFertilizer_N = 18;
            totalOptFertilizer_P = 10;
            totalOptFertilizer_K = 20;
        }

        #region Other ExpandFertilizer Method
        private float CheckFertilizer()
        {
            /*
            // show Fertilizer Message         
            if ((optFertilizer_N - offsetFertilizer_N) < curFertilizer_N && curFertilizer_N < (optFertilizer_N - offsetFertilizer_N)
             && (optFertilizer_P - offsetFertilizer_P) < curFertilizer_P && curFertilizer_P < (optFertilizer_P - offsetFertilizer_P)
             && (optFertilizer_K - offsetFertilizer_K) < curFertilizer_K && curFertilizer_K < (optFertilizer_K - offsetFertilizer_K))
                echo("Fertilizer Right!");
            else
                echo("Fertilizer Error!");
            */

            // cal coeWater
            double Y = 379.049 + 37.1959 * curFertilizer_N - 6.2586 * curFertilizer_P + 24.0806 * curFertilizer_K + 0.2828 * curFertilizer_N * curFertilizer_P - 0.9283 * curFertilizer_N * curFertilizer_K + 0.4025 * curFertilizer_P * curFertilizer_K - 1.6603 * curFertilizer_N * curFertilizer_N - 0.1614 * curFertilizer_P * curFertilizer_P - 1.0041 * curFertilizer_K * curFertilizer_K;
            double optY = 450;
            float coe = Convert.ToSingle(Y / optY);

            return coe;
        }

        // set
        private void getOptFertilizerandOffsetFertilizer()
        {
            // get OptFertilizer
            if (this.ISTAGE == 7 || this.ISTAGE == 8)
            {
                optFertilizer_N = XBIOMAS / 100 * 3.1f * 0.2f;
                optFertilizer_P = XBIOMAS / 100 * 1.4f * 0.15f;
                optFertilizer_K = XBIOMAS / 100 * 2.8f * 0.15f;
            }
            else if (this.ISTAGE == 9 || this.ISTAGE == 1 || this.ISTAGE == 2)
            {
                optFertilizer_N = XBIOMAS / 100 * 3.1f * 0.2f;
                optFertilizer_P = XBIOMAS / 100 * 1.4f * 0.15f;
                optFertilizer_K = XBIOMAS / 100 * 2.8f * 0.15f;
            }
            else if (this.ISTAGE == 3 || this.ISTAGE == 4)
            {
                optFertilizer_N = XBIOMAS / 100 * 3.1f * 0.45f;
                optFertilizer_P = XBIOMAS / 100 * 1.4f * 0.25f;
                optFertilizer_K = XBIOMAS / 100 * 2.8f * 0.7f;
            }
            else if (this.ISTAGE == 5 || this.ISTAGE == 6)
            {
                optFertilizer_N = XBIOMAS / 100 * 3.1f * 0.35f;
                optFertilizer_P = XBIOMAS / 100 * 1.4f * 0.60f;
                optFertilizer_K = XBIOMAS / 100 * 2.8f * 0.15f;
            }
            // get offsetFertilizer
            offsetFertilizer_N = optFertilizer_N * 0.05f;
            offsetFertilizer_P = optFertilizer_P * 0.05f;
            offsetFertilizer_K = optFertilizer_K * 0.05f;
        }
        #endregion
        #endregion

        #region ExpandIrrigation 接口成员
        private float curWater = 0;     // current Water add by User Action : Irrigation 
        private float optWater = 0;     // optimization Water Different with Maize ISTAGE   
        private float avgWater = 0;   // average Water : sum of optWater see function getOptWaterandOffsetWater()
        private float offsetWater = 0;  // offset Water 

        private float totalCurWater = 0;// sum of curWater
        private float totalOptWater = 0;// sum of optWater   
        public float CalIrrigation()
        {
            //SetOptWaterAndOffsetWater(); // 依据生长阶段设置最佳灌溉量，是否需要？？
            return CheckIrrigation();
        }

        public void DoIrrigation(float water)
        {
            curWater += water;
            totalCurWater += water;
        }

        // call by Construct
        public void InitIrrigation()
        {
            DoIrrigation(farm.soilInfo.Water);
            avgWater = 405;
            totalOptWater = 305;
        }

        #region Other ExpandIrrigation Method
        private float CheckIrrigation()
        {
            /*
            // show Irrigation Message         
            if ((optWater - offsetWater) < curWater && curWater < (optWater + offsetWater))
                echo("Irrigation Right!");
            else
                echo("Irrigation Error!");
            */

            // cal coeWater
            float W = (totalCurWater - totalOptWater) / avgWater;
            float coe = 0.997f + 0.0105f * W - 0.94396f * W * W;

            return coe;
        }

        // set
        private void SetOptWaterAndOffsetWater()
        {
            if (this.ISTAGE == 7 || this.ISTAGE == 8)
            {
                optWater = 15; //[10,20]
                offsetWater = 5;
            }
            else if (this.ISTAGE == 9 || this.ISTAGE == 1 || this.ISTAGE == 2)
            {
                optWater = 90; //[75,100]
                offsetWater = 10;
            }
            else if (this.ISTAGE == 3 || this.ISTAGE == 4)
            {
                optWater = 110; //[100,120]  
                offsetWater = 10;
            }
            else if (this.ISTAGE == 5 || this.ISTAGE == 6)
            {
                optWater = 90; //[60,120] 
                offsetWater = 30;
            }
        }
        #endregion
        #endregion

        #region CCrop 成员函数
        override public void DailyUpdate()
        {
            coeIrrigation = CalIrrigation();  // Must Implement InitIrrigation() first , Init at Constructer 
            coeFertilizer = CalFertilizer();   // Must Implement InitFertilizer() first , Init at Constructer 

            Routine();

        }

        override public bool IsReap()
        {
            if (this.ISTAGE == 6 || ISTAGE == 0)
                return true;

            return false;
        }

        // 产量预测，使用当前对象进行克隆，然后进行预测
        override public CCropState Predict()
        {
            // 当作物已经成熟，不能再预测

            if (this.ISTAGE == 6 || ISTAGE == 0)
            {
                CGameInfo.GetInstance().AddInfo(farm.farmName + "上的作物已经成熟,无法预测");
                return null;
            }

            // 预测迭代计算
            CeresMaize_Logic maizeClone = (CeresMaize_Logic)Clone();
            while (maizeClone.ISTAGE != 6)
            {
                // 使用真实的计算过程，预测准确
                //maizeClone.DailyUpdate();
                // 使用当前的灌溉与施肥数据进行预测，造成预测不准确
                maizeClone.Routine();
            }

            // 返回预测结果
            CMaizeState maizeState = new CMaizeState(maizeClone);
            return maizeState;
        }

        // 完成深拷贝，用于预测使用
        override public CCrop Clone()
        {
            CeresMaize_Logic clone = new CeresMaize_Logic(farm);
            clone.avgWater = this.avgWater;
            clone.BIOMAS = this.BIOMAS;
            clone.CARBO = this.CARBO;
            clone.coeFertilizer = this.coeFertilizer;
            clone.coeIrrigation = this.coeIrrigation;
            clone.cropName = this.cropName;
            clone.CUMDTT = this.CUMDTT;
            clone.CUMPH = this.CUMPH;
            clone.curFertilizer_K = this.curFertilizer_K;
            clone.curFertilizer_N = this.curFertilizer_N;
            clone.curFertilizer_P = this.curFertilizer_P;
            clone.curWater = this.curWater;
            clone.DEC = this.DEC;
            clone.DLAYR = this.DLAYR;
            clone.DLV = this.DLV;
            clone.DTT = this.DTT;
            clone.DUL = this.DUL;
            clone.EARS=this.EARS;
            clone.EARWT=this.EARWT;
            clone.farm=this.farm;
            clone.G2=this.G2;
            clone.G3=this.G3;
            clone.GPP = this.GPP;
            clone.GPSM = this.GPSM;
            clone.GRNWT = this.GRNWT;
            clone.GROEAR = this.GROEAR;
            clone.GROGRN = this.GROGRN;
            clone.GROLF = this.GROLF;
            clone.GRORT = this.GRORT;
            clone.GROSTM = this.GROSTM;
            clone.HRLT = this.HRLT;
            clone.IDURP = this.IDURP;
            clone.INSOIL = this.INSOIL;
            clone.ISDATE = this.ISDATE;
            clone.ISOW = this.ISOW;
            clone.ISTAGE = this.ISTAGE;
            clone.JDATE = this.JDATE;
            clone.LAI = this.LAI;
            clone.LAT = this.LAT;
            clone.LFWT = this.LFWT;
            clone.LL = this.LL;
            clone.LWMIN = this.LWMIN;
            clone.MAXLAI = this.MAXLAI;
            clone.NAME = this.NAME;
            clone.NDAS = this.NDAS;
            clone.offsetFertilizer_K = this.offsetFertilizer_K;
            clone.offsetFertilizer_N = this.offsetFertilizer_N;
            clone.offsetFertilizer_P = this.offsetFertilizer_P;
            clone.offsetWater = this.offsetWater;
            clone.optFertilizer_K = this.optFertilizer_K;
            clone.optFertilizer_N = this.optFertilizer_N;
            clone.optFertilizer_P = this.optFertilizer_P;
            clone.optWater = this.optWater;
            clone.P1 = this.P1;
            clone.P2 = this.P2;
            clone.P3 = this.P3;
            clone.P3 = this.P5;
            clone.P9 = this.P9;
            clone.PAR = this.PAR;
            clone.PC = this.PC;
            clone.PCARB = this.PCARB;
            clone.PLA=this.PLA;
            clone.PLAG = this.PLAG;
            clone.PLANTS = this.PLANTS;
            clone.PLAS = this.PLAS;
            clone.PRFT = this.PRFT;
            clone.PSKER = this.PSKER;
            clone.PTF = this.PTF;
            clone.RAIN = this.RAIN;
            clone.RATEIN = this.RATEIN;
            clone.RGFILL = this.RGFILL;
            clone.RTDEP = this.RTDEP;
            clone.RTWT = this.RTWT;
            clone.SAT = this.SAT;
            clone.SDEPTH = this.SDEPTH;
            clone.SEEDRV = this.SEEDRV;
            clone.SENLA = this.SENLA;
            clone.SIND = this.SIND;
            clone.SLAN = this.SLAN;
            clone.SLFC = this.SLFC;
            clone.SLFT = this.SLFT;
            clone.SLFW = this.SLFW;
            clone.SOLRAD = this.SOLRAD;
            clone.STMWT = this.STMWT;
            clone.SUMDTT = this.SUMDTT;
            clone.SUMP = this.SUMP;
            clone.SW = this.SW;
            clone.SWDF1 = this.SWDF1;
            clone.SWDF2 = this.SWDF2;
            clone.SWMAX = this.SWMAX;
            clone.SWMIN = this.SWMIN;
            clone.SWSD = this.SWSD;
            clone.TBASE = this.TBASE;
            clone.TEMPM = this.TEMPM;
            clone.TEMPMN = this.TEMPMN;
            clone.TEMPMX = this.TEMPMX;
            clone.TI = this.TI;
            clone.TLNO = this.TLNO;
            clone.TMFAC = this.TMFAC;
            clone.totalCurFertilizer_K = this.totalCurFertilizer_K;
            clone.totalCurFertilizer_N = this.totalCurFertilizer_N;
            clone.totalCurFertilizer_P = this.totalCurFertilizer_P;
            clone.totalCurWater = this.totalCurWater;
            clone.totalOptFertilizer_K = this.totalOptFertilizer_K;
            clone.totalOptFertilizer_N = this.totalOptFertilizer_N;
            clone.totalOptFertilizer_P = this.totalOptFertilizer_P;
            clone.totalOptWater = this.totalOptWater;
            clone.WR = this.WR;
            clone.XBIOMAS = this.XBIOMAS;
            clone.XLFWT = this.XLFWT;
            clone.XN = this.XN;
            clone.XNTI = XNTI;

            return clone;
        }

        override public CCropState GetState()
        {
            CMaizeState maizeState = new CMaizeState(this);
            return maizeState;
        }
        #endregion



        public CeresMaize_Logic(CFarm farm)
            : base(farm)
        {
            base.cropName = "玉米";

            // test data
            // line 1
            //TITLE = "";
            ISOW = 0;
            PLANTS = 5.7f;
            SDEPTH = 5;
            LAT = 39;
            //KOUTWA = 7;
            //KOUTGR = 7;
            //IIRR = true;
            INSOIL = 1;
            //ISWSWB = true;

            //line 2
            NAME = "";
            P1 = 175;
            P2 = 0.52f;
            P5 = 630;
            G2 = 936;
            G3 = 8;
            XBIOMAS = 500;
            // line 3
            // null
            // line 4
            //SALB = 0.14f;
            //U = 6;
            //SWCON = 0.3f;
            //CN2 = 60;
            //soil layer infor
            DLAYR[0] = 15; LL[0] = 0.16f; DUL[0] = 0.315f; SAT[0] = 0.37f; WR[0] = 0.93f; SW[0] = 0;
            DLAYR[1] = 15; LL[1] = 0.16f; DUL[1] = 0.315f; SAT[1] = 0.37f; WR[1] = 0.70f; SW[1] = 0;
            for (int i = 0; i < 10; i++)
            {
                // INSOIL
                if (INSOIL == 0)
                    SW[i] = LL[i];
                else if (INSOIL == 1)
                    SW[i] = DUL[i];
                else if (0 < INSOIL && INSOIL < 1.0)
                    SW[i] = INSOIL * (DUL[i] - LL[i]);
                else
                    break;
            }
            //irri infor
            //JDAY = 187;
            //AIRR = 32;


            // Weather Infor
            // Read Form File
            for (int i = 0; i < 200; i++)
            {
                /*
                if (i < 500)
                {
                    TEMPMN[i] = 12+rand()*3/RAND_MAX;
                    TEMPMX[i] = 27+rand()*3/RAND_MAX;
                }
                else if ( i < 100)
                {
                    TEMPMN[i] = 15+rand()*3/RAND_MAX;
                    TEMPMX[i] = 27+rand()*3/RAND_MAX;
                }
                else 
                {
                    TEMPMN[i] = 15+rand()*3/RAND_MAX;
                    TEMPMX[i] = 25+rand()*3/RAND_MAX;
                }

                JDATE[i]=ISOW+i;
                SOLRAD[i] = 500+rand()*100/RAND_MAX;

                RAIN[i] = rand()/RAND_MAX;
                */
                TEMPMN[i] = 15;
                TEMPMX[i] = 25;
                SOLRAD[i] = 500;
                RAIN[i] = 0.1f;
            }

            ISTAGE = 7;
            NDAS = 0;

            // init ISTAGE=7
            //null

            // init ISTAGE=8
            SWSD = 0;
            P9 = 0;

            // init ISTAGE=9
            RTDEP = SDEPTH;	// 0 or SDEPTH ??
            TBASE = 0;
            TEMPM = 0;
            DTT = 0;
            CUMDTT = 0;
            SUMDTT = 0;



            // init ISTAGE=1
            // null

            // init ISTAGE=2
            DEC = 0;
            DLV = 0;
            HRLT = 0;
            RATEIN = 0;
            SIND = 0;
            P3 = 0;
            TLNO = 0;

            //init ISTAGE=3
            ISDATE = 0;
            MAXLAI = 0;
            EARWT = 0;
            STMWT = 0;
            SWMIN = 0;

            //init ISTAGE=4
            PSKER = 0;
            GPP = 0;
            GPSM = 0;
            EARS = 0;
            SWMAX = 0;
            LWMIN = 0;

            // init ISTAGE=5
            // null

            // init ISTAGE=6
            // null

            // init growSub
            XN = 0;
            PC = 0;
            CUMPH = 0;
            CARBO = 0;
            LAI = 0;
            LFWT = 0;
            PAR = 0;
            PCARB = 0;
            PLA = 0;
            PLAG = 0;
            BIOMAS = 0;
            GRNWT = 0;
            GROEAR = 0;
            GROGRN = 0;
            GROLF = 0;
            GRORT = 0;
            GROSTM = 0;
            IDURP = 0;
            PLAS = 0;
            PRFT = 0;
            RGFILL = 0;
            SENLA = 0;
            SLFC = 0;
            SLFT = 0;
            SLFW = 0;
            SUMP = 0;
            TI = 0;
            XLFWT = 0;
            XNTI = 0;
            RTWT = 0;
            PTF = 0;

            SWDF1 = 1.0f;
            SWDF2 = 1.0f;

        }



        #region CeresMaize_Attribute
        // Input File Data , order by statements
        // from Parameter

        // line 1
        // String TITLE;

        // line 2
        public int ISOW;    // Day of Seminate
        float PLANTS;
        float SDEPTH;
        float LAT;
        //int KOUTWA; // never use
        //int KOUTGR; // never use
        //bool IIRR; // never use
        float INSOIL;
        //bool ISWSWB; // never use

        // line 3
        string NAME; // never use
        float P1;
        float P2;
        float P5;
        float G2;
        float G3;
        float XBIOMAS;  //  read from Seed

        // line 4
        //int ISLKJD; // never use
        //int MATJD; // never use
        //float XYIELD; // never use
        //float XGRWT; // never use
        //float XGPSM; // never use
        //float XGGPE; // never use
        //float XLAI; // never use
        //float XBIOM; // never use

        // line 5
        //float SALB; // never use
        //float U; // never use
        //float SWCON; // never use
        //float CN2; // never use
        // Soil Layer Information
        float[] DLAYR = new float[10];
        float[] LL = new float[10];
        float[] DUL = new float[10];
        float[] SAT = new float[10];
        float[] WR = new float[10];
        float[] SW = new float[10];
        // Irrigation Information
        //int JDAY; // never use
        // float AIRR; // never use

        // from Weather
        public int[] JDATE = new int[365];
        public float[] SOLRAD = new float[365];
        public float[] TEMPMX = new float[365];
        public float[] TEMPMN = new float[365];
        public float[] RAIN = new float[365];

        // Kernel Logical Data
        //
        public int ISTAGE;
        public int NDAS;

        float[] TMFAC = new float[8];

        float TBASE;
        float TEMPM;

        float DTT;
        float CUMDTT;
        float SUMDTT;

        public float BIOMAS;
        float CARBO;
        float CUMPH;
        float DEC;
        float DLV;
        float EARS;
        public float EARWT;
        float GPP;
        float GPSM;
        public float GRNWT;
        float GROEAR;
        float GROGRN;
        float GROLF;
        float GRORT;
        float GROSTM;
        float HRLT;
        float IDURP;
        int ISDATE;
        float LAI;
        public float LFWT;
        float LWMIN;
        float MAXLAI;
        float P3;
        float P9;
        float PAR;
        float PC;
        float PCARB;
        float PLA;
        float PLAG;
        float PLAS;
        float PRFT;
        float PSKER;
        float RATEIN;
        float RGFILL;
        float RTDEP;	// root depth
        float SENLA;
        float SIND;
        float SLAN;
        float SLFC;
        float SLFT;
        float SLFW;
        public float STMWT; // 
        float SUMP;
        float SWDF1;
        float SWDF2;
        float SWMAX;
        float SWMIN;
        float SWSD;
        float TI;
        int TLNO;
        float XLFWT;
        float XN;
        float XNTI;
        float SEEDRV;	//TODO: init?
        public float RTWT;
        float PTF;

        #endregion


        #region CeresMaize_Core Function
        protected void Routine()
        {
            //while (broutine)
            //{
            switch (ISTAGE)
            {
                case 1:
                    inISTAGE_1();
                    break;
                case 2:
                    inISTAGE_2();
                    break;
                case 3:
                    inISTAGE_3();
                    break;
                case 4:
                    inISTAGE_4();
                    break;
                case 5:
                    inISTAGE_5();
                    break;
                case 6:
                    inISTAGE_6();
                    break;
                case 7:
                    inISTAGE_7();
                    break;
                case 8:
                    inISTAGE_8();
                    break;
                case 9:
                    inISTAGE_9();
                    break;
                case 0:
                    inISTAGE_0();
                    break;
            }
        }

        protected void dailyCal()
        {
            DTTCal();

            GROWNCal();

            SUMDTTCal();
            NDAS++;

        }

        protected void DTTCal()
        {
            DTT = 0;
            // cal begin at ISTAGE=9
            if (ISTAGE != 7 && ISTAGE != 8)
            {
                TEMPM = (TEMPMX[(ISOW + NDAS)%365] + TEMPMN[(ISOW + NDAS)%365]) / 2;

                // initial TABSE
                if (ISTAGE == 9)
                    TBASE = 10;
                else
                    TBASE = 8;

                if (TBASE < TEMPMN[(ISOW + NDAS)%365] && TEMPMX[(ISOW + NDAS)%365] < 34)
                    DTT = TEMPM - TBASE;
                else if (TEMPMN[(ISOW + NDAS)%365] < TBASE || TEMPMX[(ISOW + NDAS)%365] > 34)
                {
                    float[] tempTTMP = new float[8];
                    float[] tempDTT = new float[8];
                    float tempDTTSum = 0.0f;
                    for (int i = 0; i < 8; i++)
                    {
                        // cal tempTTMP[i]
                        TMFAC[i] = 0.931f + 0.114f * i - 0.0703f * i * i + 0.0053f * i * i * i;
                        tempTTMP[i] = TEMPM + TMFAC[i] * (TEMPMX[i] - TEMPMN[i]);
                        // cal tempDTT[i]
                        if (TBASE < tempTTMP[i] && tempTTMP[i] < 34)
                        {
                            tempDTT[i] = tempTTMP[i] - TBASE;
                        }
                        else if (TEMPMN[(ISOW + NDAS)%365] < tempTTMP[i] || tempTTMP[i] > 44)
                        {
                            tempDTT[i] = 0;
                        }
                        else if (34 < tempTTMP[i] && tempTTMP[i] < 44)
                        {
                            tempDTT[i] = (34 - TBASE) * (1 - (tempTTMP[i] - 34) / 10);
                        }


                        tempDTTSum += tempDTT[i];

                    }// end for

                    DTT = tempDTTSum / 8;
                }
                //else 
                //std::cout<<"DTT cal error! \n";			
            }
        }

        protected void GROWNCal()
        {
            // effective when ISTAGE [1,6]
            if (ISTAGE >= 7)
                return;

            //init
            GROEAR = 0;
            GROGRN = 0;
            GROLF = 0;
            GRORT = 0;
            GROSTM = 0;

            // daily cal
            PAR = 0.02f * SOLRAD[(ISOW + NDAS)%365];
            PCARB = Convert.ToSingle(5.0f * PAR / PLANTS * (1 - Math.Exp(-0.65f * LAI)));
            PRFT = Convert.ToSingle(1 - 0.0025f * Math.Pow(((0.25f * TEMPMN[(ISOW + NDAS)%365] + 0.75f * TEMPMX[(ISOW + NDAS)%365]) - 26), 2));

            CARBO = PCARB * Math.Min(PRFT, SWDF1); // TODO: update SWDF1

            // simulate 
            //CARBO *= CheckIrrigation()*CheckFertilizer();
            CARBO *= coeFertilizer * coeIrrigation;

            // leaf growing 
            if (ISTAGE <= 3)
            {
                XN = CUMPH;
                if (CUMPH < 5)
                    PC = 0.66f + 0.068f * CUMPH;
                else
                    PC = 1;
                TI = DTT / (38.9f * PC);
                CUMPH = CUMPH + TI;
                //XN = CUMPH + 1;
            }

            if (ISTAGE <= 2)
            {
                LFWT = XLFWT;

                if (XN < 4.0f)
                    PLAG = 3.0f * XN * TI * SWDF2;
                else
                    PLAG = 3.5f * XN * XN * TI * SWDF2;

                // leaf
                PLA += PLAG;
                SLAN = SUMDTT * PLA / 10000;

                XLFWT = Convert.ToSingle(Math.Pow((PLA / 267), 1.25f));
                GROLF = XLFWT - LFWT;
                GRORT = CARBO - GROLF;

                if (GRORT < CARBO * 0.25f && SEEDRV != 0)
                {
                    GRORT = CARBO * 0.25f;
                    SEEDRV += CARBO - GROLF - GRORT;
                    PLA = Convert.ToSingle(Math.Pow((LFWT + GROLF), 0.8f) * 267);
                    XLFWT = Convert.ToSingle(Math.Pow((PLA / 267), 1.25f));
                }

                RTWT += GRORT;
            }

            if (ISTAGE == 3)
            {
                if (XN < 12)
                {
                    PLAG = 3.5f * XN * XN * TI * SWDF2;
                    GROLF = Convert.ToSingle(0.00116f * PLAG * Math.Pow(PLA, 0.25f));
                    GROSTM = Convert.ToSingle(GROLF * 0.0182f * Math.Pow((XN - XNTI), 2));
                }
                else if (12 < XN && XN < TLNO - 3)
                {
                    PLAG = 3.5f * 170 * TI * SWDF2;
                    GROLF = Convert.ToSingle(0.00116f * PLAG * Math.Pow(PLA, 0.25f));
                    GROSTM = Convert.ToSingle(GROLF * 0.0182f * Math.Pow((XN - XNTI), 2));
                }
                else if (TLNO - 3 <= XN && XN <= TLNO)
                {
                    PLAG = Convert.ToSingle(170 * 3.5f / (Math.Pow((XN + 5 - TLNO), 0.5f) * TI * SWDF2));
                    GROLF = Convert.ToSingle(0.00116f * PLAG * Math.Pow(PLA, 0.25f));
                    GROSTM = 3.1f * 3.5f * TI * SWDF2;
                }

                GRORT = CARBO - GROLF - GROSTM;

                if (GRORT < CARBO * 0.1f)
                {
                    GRORT = CARBO * 0.1f;
                    float GRF = CARBO * 0.9f / (GROSTM + GROLF);
                    GROLF -= GRF;
                    GROSTM -= GRF;
                }
                // leaf
                PLA += PLAG;
                SLAN = PLA / 1000;
            }

            if (ISTAGE == 4)
            {
                GROEAR = 0.22f * DTT * SWDF2;
                GROSTM = GROEAR * 0.4f;

                if (GRORT > CARBO * 0.08f)
                    GRORT = CARBO - GROEAR - GROSTM;
                else
                {
                    GRORT = CARBO * 0.08f;
                    float GRF = CARBO * 0.92f / (GROSTM + GROEAR);
                    GROEAR -= GRF;
                    GROSTM -= GRF;
                }

                SUMP += CARBO;
                IDURP++;

                // leaf
                SLAN = PLA * (0.05f + SUMDTT / 170 * 0.05f);
            }

            if (ISTAGE == 5)
            {
                float[] tempTTMP = new float[8];
                RGFILL = 0;
                for (int i = 0; i < 8; i++)
                {
                    // cal tempTTMP[i]
                    TMFAC[i] = 0.931f + 0.114f * i - 0.0703f * i * i + 0.0053f * i * i * i;
                    tempTTMP[i] = TEMPM + TMFAC[i] * (TEMPMX[(ISOW + NDAS)%365] - TEMPMN[(ISOW + NDAS)%365]);
                    if (tempTTMP[i] >= 6)
                        RGFILL += Convert.ToSingle((1 - 0.0025f * Math.Pow((tempTTMP[i] - 26), 2)) / 8);
                    //RGFILL += Convert.ToSingle((1.0f - 0.0025f * Math.Pow((tempTTMP[i] - 26), 2)) / 8.0f);
                }
                GROGRN = RGFILL * GPP * G3 * 0.001f * (0.45f + 0.55f * SWDF1);
                GROSTM = (CARBO - GROGRN) * 0.5f;
                GRORT = GROSTM;

                SLAN = Convert.ToSingle(PLA * (0.1f + 0.8f * Math.Pow((SUMDTT / P5), 3)));
            }

            // daily cal
            GRNWT += GROGRN;
            EARWT += GROEAR;
            STMWT += GROSTM;
            RTWT = RTWT + 0.5f * GRORT - 0.005f * RTWT;
            LFWT += GROLF;
            // leaf weight = XLFWT ; cal before

            SLFW = 0.95f + 0.05f * SWDF1;
            SLFC = 1;
            if (LAI > 4)
                SLFC = 1 - 0.008f * (LAI - 4);
            SLFT = 1 - (6 - TEMPM) / 6;
            if (TEMPM < 0)
                SLFT = 0;

            // leaf
            PLAS = (PLA - SENLA) * (1 - Math.Min(SLFW, Math.Min(SLFC, SLFT)));
            SENLA += PLAS;
            if (SENLA < SLAN)
                SENLA = SLAN;

            LAI = (PLA - SENLA) * PLANTS * 0.0001f;
            BIOMAS = (LFWT + STMWT + EARWT) * PLANTS;
            PTF = (LFWT + STMWT + EARWT) / (LFWT + STMWT + EARWT + RTWT);
        }


        protected void SUMDTTCal()
        {
            if (ISTAGE != 7 && ISTAGE != 8)
            {
                SUMDTT += DTT;
                CUMDTT += DTT;
            }
        }

        protected void inISTAGE_7()
        {

            // to end 
            ISTAGE = 8;
        }

        protected void inISTAGE_8()
        {
            dailyCal();

            SWSD = (SW[0] - LL[0]) * 0.65f + (SW[1] - LL[1]) * 0.35f;

            // to end 
            if (SWSD >= 0.02)
            {
                ISTAGE = 9;
                P9 = 15 + 6 * SDEPTH;
            }
            if (NDAS == 40)
            {
                return; 
            }
        }

        protected void inISTAGE_9()
        {
            dailyCal();

            RTDEP += 0.15f * DTT;	// root depth

            // to end 
            if (SUMDTT >= P9)
            {
                ISTAGE = 1;
                SUMDTT = 0;	// SUMDTT reset
            }
        }

        protected void inISTAGE_1()
        {
            dailyCal();

            // to end 
            if (SUMDTT >= P1)
            {
                ISTAGE = 2;
                SEEDRV = 0;
            }
        }

        protected void inISTAGE_2()
        {
            dailyCal();

            float pi = 3.1415926f;

            DEC = Convert.ToSingle(0.4093f * Math.Sin(0.0172f * (JDATE[(ISOW + NDAS)%365] - 82.2f) * pi));
            DLV = Convert.ToSingle((-Math.Sin(LAT / 90) * Math.Sin(DEC) - 0.1047f) / (Math.Cos(LAT / 90) * Math.Cos(DEC)));
            HRLT = Convert.ToSingle(7.639f * Math.Acos(DLV));

            if (HRLT < 12.5f)
            {
                HRLT = 12.5f;
            }
            RATEIN = 1 / (4 + P2 * (HRLT - 12.5f));

            SIND += RATEIN;

            // to end 
            if (SIND >= 1.0)
            {
                ISTAGE = 3;
                TLNO = (int)(SUMDTT / 21 + 6);
                XNTI = XN;
                P3 = (TLNO - 2) * 38.9f + 96 - SUMDTT;

                SUMDTT = 0;
            }

        }

        protected void inISTAGE_3()
        {
            dailyCal();

            // to end 
            if (SUMDTT >= P3)
            {
                ISTAGE = 4;
                ISDATE = (ISOW + NDAS)%365;
                MAXLAI = LAI;
                // GROWNSub
                EARWT = 0.167f * STMWT;
                STMWT = STMWT - EARWT;
                SWMIN = STMWT * 0.8f;

                SUMDTT = 0;
            }

        }

        protected void inISTAGE_4()
        {
            dailyCal();

            // to end 
            if (SUMDTT >= 170)
            {
                ISTAGE = 5;

                PSKER = SUMP * 1000 / IDURP * 3.4f / 5.0f;	//TODO: update SUMP IDUP
                GPP = G2 * (PSKER - 195) / (1213.2f + PSKER - 195);
                if (GPP >= 0.55f)
                {
                    GPSM = GPP * PLANTS;
                }
                else if (0.5 <= GPP && GPP < 0.55f)
                {
                    EARS = Convert.ToSingle(PLANTS * Math.Pow(((GPP - 50) / (G2 - 50)), 0.33f));
                    GPSM = GPP * EARS;
                }
                else
                    GPSM = 0;

                SWMAX = STMWT;
                LWMIN = LFWT * 0.85f;
            }
        }

        protected void inISTAGE_5()
        {
            dailyCal();

            // to end 
            if (SUMDTT >= P5 * 0.95f)
            {
                ISTAGE = 6;

                // reset SWMAX
                if (STMWT > SWMAX)
                    SWMAX = STMWT;
            }

        }

        protected void inISTAGE_6()
        {
            dailyCal();

            // to end 
            if (SUMDTT >= P5 || DTT < 2.0f)
            {

                // 作物成熟
                CGameInfo.GetInstance().AddInfo(farm.farmName + "上的" + cropName + "作物已经成熟");
                ISTAGE = 0;

            }
        }

        // 扩展 成熟以后的处理方式
        protected void inISTAGE_0()
        {
            NDAS++;

            // reduce every day
            BIOMAS *= 0.99f;
            RTWT *= 0.99f;
            STMWT *= 0.99f;
            LFWT *= 0.99f;
            EARWT *= 0.99f;
            GRNWT *= 0.99f;
        }
        #endregion


    }

