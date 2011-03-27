using UnityEngine;
using System.Collections.Generic;

// ��U3D��ϵ�����״̬GUI
public class GUI_Tip : MonoBehaviour
{
    public Texture[] texMaizeState;  // ����״̬����ͼ����Ϊ4��״̬����0-3���Դ��ǲ����� ��ѿ�� ������ ������

    int tipID;       // Tip���ڱ�ţ�Ŀǰ�У�0,99��
    object obj;    // Tip�����е��������ã�������string �� CFarm
    bool bShow ;

    static GUI_Tip instance; // ʹ�÷�����GUI_Tip.GetInstance().OpenTip(id,obj)

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


        if (tipID == 99)   // 99 tip��ũ��tip
        {
            if ((CFarm)obj == null || ((CFarm)obj).crop == null)
                return;

            // Get State from Farm       
            if (((CFarm)obj).crop.cropName == "����")
            {
                CMaizeState state = (CMaizeState)(((CFarm)obj).GetState());
                if (state != null)
                    ShowMaizeTip(state);
            }
        }
        else if (tipID == 0) // 0 tip����ͨ��Ϣ��ʾ
        {
            ShowCommonTip((string)obj);
        }
    }


    void ShowCommonTip(string str)
    {
        // ���
        //GUI.Box(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 200, 150),"");
        GUILayout.BeginArea(new Rect(Input.mousePosition.x + 10, Screen.height - Input.mousePosition.y + 20, 200, 150));
        GUILayout.Label(str);
        GUILayout.EndArea();
    }

    void ShowMaizeTip(CMaizeState state)
    {
        // ���
        GUI.Box(new Rect(Input.mousePosition.x,Screen.height-Input.mousePosition.y, 400, 185), ((CFarm)obj).crop.cropName + "������Ϣ");
        
        GUILayout.BeginArea(new Rect(Input.mousePosition.x+10, Screen.height - Input.mousePosition.y + 20, 100, 150));
        GUILayout.Label("�� �� :" + state.cropName, GUILayout.Height(35));
        GUILayout.Label("������:" + state.BIOMAS, GUILayout.Height(35));
        GUILayout.Label("�� �� : " + state.EARWT, GUILayout.Height(35));
        GUILayout.Label("�� �� :" + state.RTWT, GUILayout.Height(35));
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Input.mousePosition.x+110, Screen.height - Input.mousePosition.y + 20, 100, 150));
        GUILayout.Label("�� �� :" + state.NDAS,GUILayout.Height(35));
        GUILayout.Label("������:" + state.GRNWT, GUILayout.Height(35));
        GUILayout.Label("�� �� :" + state.STMWT, GUILayout.Height(35));
        GUILayout.Label("Ҷ �� :" + state.LFWT, GUILayout.Height(35));
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Input.mousePosition.x + 210, Screen.height - Input.mousePosition.y + 20, 150, 150));
        
        // ��������״̬��ʾͼƬ
        //   ISTAGE == 7 || == 8           ������ 
        //   ISTAGE == 9 || == 1 ||  == 2     ��ѿ��
        //   ISTAGE == 3 || == 4       ������
        //   ISTAGE == 5 || == 6       ������
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

    // ����thisTipID��thisObjΪTip��ֵ����OnGUI()���Զ����ö�ӦTip
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
