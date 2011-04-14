using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CeresMaize_Console_CS
{
    public enum CSeedType
    {
        Maize_Normal,
        Maize_High
    }
    public class CSeed
    {
        public CSeedType type; // 种类，目前仅实现了MaizeSeed
        public string des;

        public CSeed(CSeedType thisType)
        {
            type = thisType;
            initByType(type);

        }

        void initByType(CSeedType type)
        {
            switch (type)
            {
                case CSeedType.Maize_Normal:
                    {
                        des = "玉米种子";
                        break;
                    }
            }
        }
    }
}
