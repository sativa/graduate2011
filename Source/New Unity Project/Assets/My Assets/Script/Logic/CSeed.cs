using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class CSeed
    {
        public string type; // 种类，目前仅实现了MaizeSeed
        public string des;
        
        public CSeed(string thisType)
        {
            type = thisType;

            initByType(type);

        }

        void initByType(string type)
        {
            if (type == "MaizeSeed")
            {
                des = "玉米种子";
            }
        }
    }

