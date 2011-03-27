using UnityEngine;
using System.Collections.Generic;
using System;

// ʱ�������
public class Control_Time : MonoBehaviour
{
    public static DateTime dt = DateTime.Now;  // TODO����Ӧ���ǽ���
    #region internal Time
    public float interTime = 3.0f;  // �������
    private float sumDeltatime = 0.0f;
    #endregion

    // TimeControl Object
    // Farm 
    List<Mod_Farm> farmList;
    // Use this for initialization
    void Start()
    {
        // init TimeControl Object
        // Farm
        farmList = new List<Mod_Farm>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Farm");
        foreach (GameObject obj in objs)
            farmList.Add(obj.GetComponent<Mod_Farm>());

        Debug.Log("InitFarm");
    }

    // Update is called once per frame
    void Update()
    {
        
        sumDeltatime += Time.deltaTime;
        if (sumDeltatime >= interTime)
        {
            //Debug.Log("TimeToUpdate:" + sumDeltatime);
            sumDeltatime = 0;

            dt=dt.AddDays(1);
            //CGameInfo.GetInstance().AddInfo(dt.ToString());

            DailyUpdate(); // Update            
        }
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