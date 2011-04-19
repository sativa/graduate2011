using System;
using System.Collections.Generic;

// 时间控制类
public class Control_Time 
{
    public static DateTime dt = DateTime.Now;  // TODO：不应该是今天
    public static ESeason season = ESeason.Spring;

    #region internal Time
    public float interTime = 3.0f;  // 触发间隔
    private float sumDeltatime = 0.0f;
    #endregion

    // TimeControl Object
    // Farm 
    List<Mod_Farm> farmList;
    // Use this for initialization
    void Start()
    {
        // init TimeControl Object
        // Terrain
        CTerrain.GetInstance().Init("1.terrain");

        /*
        // Farm
        farmList = new List<Mod_Farm>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Farm");
        foreach (GameObject obj in objs)
            farmList.Add(obj.GetComponent<Mod_Farm>());
        */
        

    }

    // Update is called once per frame
    void Update()
    {
        /*
        sumDeltatime += Time.deltaTime;
        if (sumDeltatime >= interTime)
        {
            //Debug.Log("TimeToUpdate:" + sumDeltatime);
            sumDeltatime = 0;

            dt=dt.AddDays(1);
            //CGameInfo.GetInstance().AddInfo(dt.ToString());
            if (3 < dt.Month && dt.Month < 6)
                season = ESeason.Summer;

            DailyUpdate(); // Update            
        }
         * */
    }

    // TimeControl Object Update
    void DailyUpdate()
    {
        foreach (Mod_Farm modFarm in farmList)
        {
            if (modFarm != null)  // Always true
            {
                modFarm.farm.DailyUpdate();                 
            }
                   
        }
    }
}