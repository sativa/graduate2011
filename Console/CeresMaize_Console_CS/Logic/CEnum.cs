using System;
using System.Collections.Generic;
using System.Text;


    // 所有的枚举，写在该文件

    public enum ESeason
    {
        Spring,
        Summer,
        Fall,
        Winter
    }
    public enum EMouseState
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

    public enum ESeedType
    {
        Maize_Normal,
        Maize_High
    };

    public enum EFertilizerType
    {
        Fertilizer_N,  // 氮肥       
        Fertilizer_P,  // 磷肥
        Fertilizer_K,  //  钾肥
        Fertilizer_Com,// 复合
    }

    //需要扣钱的状态
    public enum ECoinState
    {
        None,		//无状态
        Assart,		//开垦
        Seminate,	//播种
        Reap,			//收获
        Irrigation,	//灌溉
        Fertilizer,	//施肥
        Weed,		//除草
        Pet		//病虫害
    }

