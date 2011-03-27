using UnityEngine;
using System.Collections;

public class GUI_GameInfo : MonoBehaviour {
   
    public GUISkin skin;

    public Texture backGRD;
    public Texture[] texMaizeState;  // 作物状态的贴图，分为4个状态，从0-3，以此是播种期 出芽期 出穗期 成熟期

    private Rect posBackGRD;   // 详细信息的位置
    private Rect posDetail;   // 详细信息的位置
    private CFarm farm;
    private bool isShowFarmInfoGUI = false;

	// Use this for initialization
    void Start()
    {
        posBackGRD = new Rect(Screen.width * 0.7f-50, Screen.height * 0.1f, Screen.width * 0.3f - Screen.width * 0.01f+50, Screen.height * 0.58f - Screen.height * 0.025f);
        posDetail = new Rect(Screen.width * 0.7f, Screen.height * 0.1f, Screen.width * 0.3f - Screen.width * 0.01f, Screen.height * 0.58f - Screen.height * 0.025f);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = skin;
        // 先绘制详细信息，这样按钮就在该信息之上
        if (Control_Game.bShowGameInfoGUI)
        {
            // 背景图绘制
            GUI.DrawTexture(posBackGRD, backGRD,ScaleMode.StretchToFill,true);

            GUI.Window(1, posDetail, GameInfoWindow, "游戏信息");
			//GUI.Window(1, posDetail, GameInfoWindow, backGRD);
        }

        
            
 
    }

    void GameInfoWindow(int id)
    {
        GUILayout.BeginVertical();
        // 天气信息
        GUILayout.Space(5);
        GUILayout.Label("当前日期:   " + Control_Time.dt.ToString("yyyy-MM-dd"));
        GUILayout.BeginHorizontal();
        GUILayout.Label("最高气温:   " + CTerrain.GetInstance().TEMPMX[Control_Time.dt.DayOfYear % 365].ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
        GUILayout.Label("最低气温:   " + CTerrain.GetInstance().TEMPMN[Control_Time.dt.DayOfYear % 365].ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
        GUILayout.EndHorizontal();
        GUILayout.Label("日照强度:   " + CTerrain.GetInstance().SOLRAD[Control_Time.dt.DayOfYear % 365].ToString());
        
        // 农田信息
        if (isShowFarmInfoGUI)
        {
            // 农田信息
            GUILayout.Space(5);
            GUILayout.Label("农田名称:   " + farm.farmName.ToString());
            GUILayout.BeginHorizontal();
            GUILayout.Label("农田含水:   " + farm.soilWater.ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
            GUILayout.Label("农田含氮:   " + farm.soilN.ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("农田含磷:   " + farm.soilP.ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
            GUILayout.Label("农田含钾:   " + farm.soilK.ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
            GUILayout.EndHorizontal();
            if (farm.crop != null)
            {
                // 作物信息
                GUILayout.Space(5);
                GUILayout.Label("作物名称:   " + farm.crop.cropName);
                // Get State from Farm       
                if (farm.crop.cropName == "玉米")
                {
                    CMaizeState state = (CMaizeState)farm.GetState();
                    if (state != null)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("生长天数:   " + state.NDAS);   // , GUILayout.Height(35)
                        GUILayout.Label("作物根重:   " + state.RTWT, GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("作物产量:   " + state.BIOMAS, GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
                        GUILayout.Label("作物茎重:   " + state.STMWT, GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("作物谷重:   " + state.GRNWT, GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
                        GUILayout.Label("作物叶重:   " + state.LFWT, GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
                        GUILayout.EndHorizontal();
                        //GUILayout.Label("作物穗重:   " + state.EARWT);
                        // 作物状态贴图
                        int index = 0;
                        if (state.ISTAGE == 7 || state.ISTAGE == 8)
                            index = 0;
                        else if (state.ISTAGE == 9 || state.ISTAGE == 1 || state.ISTAGE == 2)
                            index = 1;
                        else if (state.ISTAGE == 3 || state.ISTAGE == 4)
                            index = 2;
                        else if (state.ISTAGE == 5 || state.ISTAGE == 6 || state.ISTAGE == 0)
                            index = 3;
                        GUILayout.Label(texMaizeState[index]);//,GUILayout.MaxWidth(240),GUILayout.MinHeight(180)
                    }
                }
            }
        }
        GUILayout.Space(5);
        GUILayout.EndVertical();
    }

    public void OpenFarmInfoGUI(CFarm thisFarm)
    {
        farm = thisFarm;
        isShowFarmInfoGUI = true;
    }

    void CloseFarmInfoGUI()
    {
        farm = null;
        isShowFarmInfoGUI = false;
    }
}
