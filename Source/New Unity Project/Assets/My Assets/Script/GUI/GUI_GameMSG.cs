using UnityEngine;
using System.Collections;

public class GUI_GameMSG : MonoBehaviour {

    private Rect pos;

    static public Vector2 scrollPosition;

	// Use this for initialization
	void Start () {
        //pos = new Rect(Screen.width * 0.5f, Screen.height * 0.66f, Screen.width * 0.5f, Screen.height * 0.3f);
        pos = new Rect(Screen.width * 0.7f, Screen.height * 0.66f, Screen.width * 0.3f - Screen.width * 0.01f, Screen.height * 0.3f - Screen.height * 0.025f);
	
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Window(0, pos, GameInfoWindow, "提示信息");
    }

    void GameInfoWindow(int id)
    {
   
        //Begin a scroll view. All rects are calculated automatically - 
        // it will use up any available screen space and make sure contents flow correctly.
        // This is kept small with the last two parameters to force scrollbars to appear.
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayout.BeginVertical();
        for (int i = 0; i < CGameInfo.GetInstance().GetInfoLength(); i++)
            GUILayout.Label(CGameInfo.GetInstance().GetInfoAt(i));

        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        GUI.DragWindow(new Rect(0, 0, 10000, 20));

    }



}
