using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CeresMaize_Console_CS
{
    // 工厂方法构造对象
    public class CFarmFactroy
    {
        static public void PlantCrop(CFarm farm,CSeed seed)
        {
            // Factroy Method
            if (seed.type == "MaizeSeed")
            {
                // Build CropStateGUI
                CMaizeStateGUI maizeGUI = new CMaizeStateGUI(farm);
                farm.stateGUI = maizeGUI;

                // Build Crop Object
                CeresMaize_Logic maizeLogic = new CeresMaize_Logic(farm);
                farm.crop=maizeLogic;
                farm.crop.cropName = maizeLogic.cropName;

                // tag WeatherInfo
                maizeLogic.JDATE = CTerrain.GetInstance().JDATE;
                maizeLogic.TEMPMN = CTerrain.GetInstance().TEMPMN;
                maizeLogic.TEMPMX = CTerrain.GetInstance().TEMPMX;
                maizeLogic.RAIN = CTerrain.GetInstance().RAIN;
                maizeLogic.SOLRAD = CTerrain.GetInstance().SOLRAD;

                // tag SemiInfo
                //maizeLogic.ISOW = Control_Time.dt.DayOfYear;
                maizeLogic.ISOW = 100;
            }
        }
    }
}
