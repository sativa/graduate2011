using UnityEngine;
using System.Collections;

public class GUI_GameInfo : MonoBehaviour {
   
    public GUISkin skin;

    public Texture backGRD;
    public Texture[] texMaizeState;  // ����״̬����ͼ����Ϊ4��״̬����0-3���Դ��ǲ����� ��ѿ�� ������ ������

    private Rect posBackGRD;   // ��ϸ��Ϣ��λ��
    private Rect posDetail;   // ��ϸ��Ϣ��λ��
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
        // �Ȼ�����ϸ��Ϣ��������ť���ڸ���Ϣ֮��
        if (Control_Game.bShowGameInfoGUI)
        {
            // ����ͼ����
            GUI.DrawTexture(posBackGRD, backGRD,ScaleMode.StretchToFill,true);

            GUI.Window(1, posDetail, GameInfoWindow, "��Ϸ��Ϣ");
			//GUI.Window(1, posDetail, GameInfoWindow, backGRD);
        }

        
            
 
    }

    void GameInfoWindow(int id)
    {
        GUILayout.BeginVertical();
        // ������Ϣ
        GUILayout.Space(5);
        GUILayout.Label("��ǰ����:   " + Control_Time.dt.ToString("yyyy-MM-dd"));
        GUILayout.BeginHorizontal();
        GUILayout.Label("�������:   " + CTerrain.GetInstance().TEMPMX[Control_Time.dt.DayOfYear % 365].ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
        GUILayout.Label("�������:   " + CTerrain.GetInstance().TEMPMN[Control_Time.dt.DayOfYear % 365].ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
        GUILayout.EndHorizontal();
        GUILayout.Label("����ǿ��:   " + CTerrain.GetInstance().SOLRAD[Control_Time.dt.DayOfYear % 365].ToString());
        
        // ũ����Ϣ
        if (isShowFarmInfoGUI)
        {
            // ũ����Ϣ
            GUILayout.Space(5);
            GUILayout.Label("ũ������:   " + farm.farmName.ToString());
            GUILayout.BeginHorizontal();
            GUILayout.Label("ũ�ﺬˮ:   " + farm.soilWater.ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
            GUILayout.Label("ũ�ﺬ��:   " + farm.soilN.ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("ũ�ﺬ��:   " + farm.soilP.ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
            GUILayout.Label("ũ�ﺬ��:   " + farm.soilK.ToString(), GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
            GUILayout.EndHorizontal();
            if (farm.crop != null)
            {
                // ������Ϣ
                GUILayout.Space(5);
                GUILayout.Label("��������:   " + farm.crop.cropName);
                // Get State from Farm       
                if (farm.crop.cropName == "����")
                {
                    CMaizeState state = (CMaizeState)farm.GetState();
                    if (state != null)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("��������:   " + state.NDAS);   // , GUILayout.Height(35)
                        GUILayout.Label("�������:   " + state.RTWT, GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("�������:   " + state.BIOMAS, GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
                        GUILayout.Label("���ﾥ��:   " + state.STMWT, GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("�������:   " + state.GRNWT, GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
                        GUILayout.Label("����Ҷ��:   " + state.LFWT, GUILayout.MaxWidth(Screen.width * 0.3f *0.49f));
                        GUILayout.EndHorizontal();
                        //GUILayout.Label("��������:   " + state.EARWT);
                        // ����״̬��ͼ
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
