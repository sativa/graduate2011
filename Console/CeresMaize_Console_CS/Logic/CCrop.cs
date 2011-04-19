using System;
using System.Collections.Generic;
using System.Text;


    // 作物抽象类，提供函数用于实现设计模式
    abstract public class CCrop
    {
        public CFarm farm;
        public string cropName;

        public float cropQuality;     // 农作物质量
        public float cropNumber;    // 农作物数量

        public float cropQualityLimit;      // 农作物质量下限
        public float cropNumberLimit;     // 农作物数量下限

        public CCrop(CFarm thisFarm)
        {
            farm = thisFarm;

            if (this is IExpandFertilizer)
            {
                ((IExpandFertilizer)this).InitFertilizer();
            }
            if (this is IExpandIrrigation)
            {
                ((IExpandIrrigation)this).InitIrrigation();
            }

        }

        abstract public void DailyUpdate();

        abstract public bool IsReap();

        abstract public CCrop Clone();   // 克隆
        abstract public CCropState Predict();

        abstract public CCropState GetState();
    }

    public interface IExpandIrrigation
    {
        float CalIrrigation();
        void DoIrrigation(float water);
        void InitIrrigation(); //农田中的水
    }

    public interface IExpandFertilizer
    {
        float CalFertilizer();
        void DoFertilizer(float n, float p, float k);
        void InitFertilizer(); // 农田中的肥料
    }
    

