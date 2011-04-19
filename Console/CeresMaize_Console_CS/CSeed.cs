using System;
using System.Collections.Generic;
using System.Text;

namespace CeresMaize_Console_CS
{

    public class CSeed
    {
        public ESeedType type; // 种类，目前仅实现了MaizeSeed
        public ESeason season; // 播种的季节
        public string des;

        public CSeed(ESeedType thisType)
        {
            type = thisType;
            initByType(type);

        }

        void initByType(ESeedType type)
        {
            switch (type)
            {
                case ESeedType.Maize_Normal:
                    {
                        des = "玉米种子";
                        season = ESeason.Spring;
                        break;
                    }
            }
        }
    }
}
