using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CeresMaize_Console_CS
{
    // 作物抽象类，提供函数用于实现设计模式
    abstract public class CCrop
    {
        public CFarm farm;
        public string cropName;

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
        abstract public CCrop Clone();   // 克隆
        abstract public CCropState Predict();

        abstract public CCropState GetState();
    }

    public interface IExpandIrrigation
    {
        float CalIrrigation();
        void DoIrrigation(int water);
        void InitIrrigation(); //农田中的水
    }

    public interface IExpandFertilizer
    {
        float CalFertilizer();
        void DoFertilizer(int n, int p, int k);
        void InitFertilizer(); // 农田中的肥料
    }
    
}
