using System;
using System.Collections.Generic;
using System.Text;

namespace CeresMaize_Console_CS
{
    // 所有的枚举，写在该文件

    public enum CMouseState
    {
        None,
        GameInfo,
        Assart,
        Seminate,
        Reap,
        Irrigation,
        Fertilizer,
        Weed,
        Pet,
        Disease

    };

    public enum CSeedType
    {
        Maize_Normal,
        Maize_High
    };

    public enum CFertilizer
    {
        Fertilizer_N,  // 氮肥       
        Fertilizer_P,  // 磷肥
        Fertilizer_K,  //  钾肥
        Fertilizer_Com,// 复合
    }

    //需要扣钱的状态
    public enum CCoinState
    {
        None,		//无状态
        Assart,		//开垦
        Seminate,	//播种
        Reap,			//收获
        Irrigation,	//灌溉
        Fertilizer,	//施肥
        Weed,		//除草
        Disease		//病虫害
    }
}
