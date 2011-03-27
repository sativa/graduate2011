using UnityEngine;
using System.Collections.Generic;

// 将农田指令放在窗口顶端，该类将作废
public class GUI_FarmGUI : MonoBehaviour
{
    CFarm farm;
    public bool bShow;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (!bShow)
            return;

        GUI.Box(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 75),"农田");

        int plus = 0;
        if (farm.crop != null)
            plus = 100;
        if (GUI.Button(new Rect(Screen.width / 2 - 50 + 100 + plus, Screen.height / 2 - 50, 50, 50), "关闭"))
        {
            SendMessage("CloseStateGUI");
            SendMessage("CloseFarmGUI");
        }

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 30, 100, 75));
        if(GUILayout.Button("MaizeSeed"))
            farm.Seminate(new CSeed("MaizeSeed"));
        GUILayout.EndArea();

    }

    public void OpenFarmGUI(CFarm thisFarm)
    {
        farm = thisFarm;
        bShow = true; 
    }

    public void CloseFarmGUI()
    {
        farm = null;
        bShow = false;
    }
}
