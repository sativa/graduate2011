using UnityEngine;
using System.Collections;

// 依据鼠标的不同状态，绘制图标
public class Control_Mouse : MonoBehaviour {

    public Texture[] texButton; // 按照按钮的顺序定义 Assart Seminate Reap Irrigation Fertilizer Weed Pet Disease
    
	// Use this for initialization
	void Start () {

        Screen.showCursor = false; // 默认关闭光标，无状态时打开
	}
	
	// Update is called once per frame
	void Update () {

        // 鼠标状态非None时，单击右键可返回None状态
        if (Control_Game.mouse.state != CMouseState.None && Input.GetMouseButtonDown(1))
            Control_Game.mouse.state = CMouseState.None;
 
	}
    void OnGUI()
    {
        GUI.depth = 0;

        // 按照按钮的顺序定义 Assart Seminate Reap Irrigation Fertilizer Weed Pet Disease
        if (Control_Game.mouse.state == CMouseState.None)
        {
            // 无
            //Screen.showCursor = true;
			//未选择状态的鼠标
			Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[8]);
        }
        else if (Control_Game.mouse.state == CMouseState.Assart)
        {
            // 开垦
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[0]);
        }
        else if (Control_Game.mouse.state == CMouseState.Seminate)
        {
            // 播种
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[1]);
        }
        else if (Control_Game.mouse.state == CMouseState.Reap)
        {
            // 收割
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[2]);
        }
        else if (Control_Game.mouse.state == CMouseState.Irrigation)
        {
            // 灌溉
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[3]);
        }
        else if (Control_Game.mouse.state == CMouseState.Fertilizer)
        {
            // 施肥
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[4]);
        }
        else if (Control_Game.mouse.state == CMouseState.Weed)
        {
            // 草害
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[5]);
        }
        else if (Control_Game.mouse.state == CMouseState.Pet)
        {
            // 虫害
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[6]);
        }
        else if (Control_Game.mouse.state == CMouseState.Disease)
        {
            // 病害
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[7]);
        }
    }

}
