using UnityEngine;
using System.Collections.Generic;

// 与U3D结合的作物状态GUI
public class GUI_Tip : MonoBehaviour
{
    public Texture[] texMaizeState;  // 作物状态的贴图，分为4个状态，从0-3，以此是播种期 出芽期 出穗期 成熟期

    int tipID;       // Tip窗口编号，目前有（0,99）
    object obj;    // Tip窗口中的数据引用，可以是string 和 CFarm
    bool bShow ;

    static GUI_Tip instance; // 使用方法：GUI_Tip.GetInstance().OpenTip(id,obj)

    static public GUI_Tip GetInstance()
    {
        return instance;
    }

	// Use this for initialization
	void Start () {
        instance = this;

        tipID = -1;    // Default -1
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (!bShow)
            return;


        if (tipID == 99)   // 99 tip是农田tip
        {
            if ((CFarm)obj == null || ((CFarm)obj).crop == null)
                return;

            // Get State from Farm       
            if (((CFarm)obj).crop.cropName == "玉米")
            {
                CMaizeState state = (CMaizeState)(((CFarm)obj).GetState());
                if (state != null)
                    ShowMaizeTip(state);
            }
        }
        else if (tipID == 0) // 0 tip是普通消息提示
        {
            ShowCommonTip((string)obj);
        }
    }


    void ShowCommonTip(string str)
    {
        // 外框
        //GUI.Box(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 200, 150),"");
        GUILayout.BeginArea(new Rect(Input.mousePosition.x + 10, Screen.height - Input.mousePosition.y + 20, 200, 150));
        GUILayout.Label(str);
        GUILayout.EndArea();
    }

    void ShowMaizeTip(CMaizeState state)
    {
        // 外框
        GUI.Box(new Rect(Input.mousePosition.x,Screen.height-Input.mousePosition.y, 400, 185), ((CFarm)obj).crop.cropName + "作物信息");
        
        GUILayout.BeginArea(new Rect(Input.mousePosition.x+10, Screen.height - Input.mousePosition.y + 20, 100, 150));
        GUILayout.Label("作 物 :" + state.cropName, GUILayout.Height(35));
        GUILayout.Label("生物量:" + state.BIOMAS, GUILayout.Height(35));
        GUILayout.Label("穗 重 : " + state.EARWT, GUILayout.Height(35));
        GUILayout.Label("根 重 :" + state.RTWT, GUILayout.Height(35));
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Input.mousePosition.x+110, Screen.height - Input.mousePosition.y + 20, 100, 150));
        GUILayout.Label("天 数 :" + state.NDAS,GUILayout.Height(35));
        GUILayout.Label("谷粒重:" + state.GRNWT, GUILayout.Height(35));
        GUILayout.Label("茎 重 :" + state.STMWT, GUILayout.Height(35));
        GUILayout.Label("叶 重 :" + state.LFWT, GUILayout.Height(35));
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Input.mousePosition.x + 210, Screen.height - Input.mousePosition.y + 20, 150, 150));
        
        // 依据作物状态显示图片
        //   ISTAGE == 7 || == 8           播种期 
        //   ISTAGE == 9 || == 1 ||  == 2     出芽期
        //   ISTAGE == 3 || == 4       出穗期
        //   ISTAGE == 5 || == 6       成熟期
        int index = 0;
        if (state.ISTAGE == 7 || state.ISTAGE == 8)
            index = 0;
        else if (state.ISTAGE == 9 || state.ISTAGE == 1 || state.ISTAGE == 2)
            index = 1;
        else if (state.ISTAGE == 3|| state.ISTAGE == 4 )
            index = 2;
        else if (state.ISTAGE == 5 || state.ISTAGE == 6 || state.ISTAGE == 0)
            index = 3;
        
        GUILayout.Label(texMaizeState[index]);
        GUILayout.EndArea();
        
    }

    // 依据thisTipID和thisObj为Tip赋值，在OnGUI()中自动调用对应Tip
    public void OpenTip(int thisTipID, object thisObj)
    {
        tipID = thisTipID;
        obj = thisObj;
        bShow = true;
    }

    public void CloseTip()
    {
        obj = null;
        bShow=false;
    }
}
