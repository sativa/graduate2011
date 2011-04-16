using System;
using System.Collections.Generic;
using System.Text;

namespace CeresMaize_Console_CS
{
    public class CCropState
    {
        public string cropName;
    }

    public class CMaizeState:CCropState
    {
        // 公开的属性
        public int NDAS;
        public int ISTAGE;

        public float BIOMAS;
        public float EARWT;
        public float GRNWT;
        public float LFWT;
        public float RTWT;
        public float STMWT;

        public CMaizeState(CeresMaize_Logic maizeLogic)
        {
            cropName = maizeLogic.cropName;

            NDAS = maizeLogic.NDAS;
            ISTAGE = maizeLogic.ISTAGE;
            // 数据截断为小数点后2位
            BIOMAS = float.Parse((maizeLogic.BIOMAS/1000*667).ToString("0.00"));
            EARWT =  float.Parse(maizeLogic.EARWT.ToString("0.00"));
            GRNWT =  float.Parse(maizeLogic.GRNWT.ToString("0.00"));
            LFWT =  float.Parse(maizeLogic.LFWT.ToString("0.00"));
            RTWT =  float.Parse(maizeLogic.RTWT.ToString("0.00"));
            STMWT = float.Parse(maizeLogic.STMWT.ToString("0.00"));

            // 由于灌溉、施肥、天气等因素，数据可能是负值
            if (BIOMAS < 0)
                BIOMAS = 0;
            if (EARWT < 0)
                EARWT = 0;
            if (GRNWT < 0)
                GRNWT = 0;
            if (LFWT < 0)
                LFWT = 0;
            if (RTWT < 0)
                RTWT = 0;
            if (STMWT < 0)
                STMWT = 0;
        }

        public override string ToString()
        {
            return "No."+NDAS.ToString()
                + " 生物量:" + BIOMAS.ToString() + ","
                + " 生长阶段:" + ISTAGE.ToString() + ","
                + " 谷粒重:" + GRNWT.ToString() + ","
                + " 根重:" + RTWT.ToString() + ","
                + " 茎重:" + STMWT.ToString() + ","
                + " 叶重:" + LFWT.ToString() + ","
                + " 穗重:" + EARWT.ToString();

             
        }
    }
}
