using UnityEngine;
using System.Collections;

// �������Ĳ�ͬ״̬������ͼ��
public class Control_Mouse : MonoBehaviour {

    public Texture[] texButton; // ���հ�ť��˳���� Assart Seminate Reap Irrigation Fertilizer Weed Pet Disease
    
	// Use this for initialization
	void Start () {

        Screen.showCursor = false; // Ĭ�Ϲرչ�꣬��״̬ʱ��
	}
	
	// Update is called once per frame
	void Update () {

        // ���״̬��Noneʱ�������Ҽ��ɷ���None״̬
        if (Control_Game.mouse.state != CMouseState.None && Input.GetMouseButtonDown(1))
            Control_Game.mouse.state = CMouseState.None;
 
	}
    void OnGUI()
    {
        GUI.depth = 0;

        // ���հ�ť��˳���� Assart Seminate Reap Irrigation Fertilizer Weed Pet Disease
        if (Control_Game.mouse.state == CMouseState.None)
        {
            // ��
            //Screen.showCursor = true;
			//δѡ��״̬�����
			Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[8]);
        }
        else if (Control_Game.mouse.state == CMouseState.Assart)
        {
            // ����
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[0]);
        }
        else if (Control_Game.mouse.state == CMouseState.Seminate)
        {
            // ����
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[1]);
        }
        else if (Control_Game.mouse.state == CMouseState.Reap)
        {
            // �ո�
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[2]);
        }
        else if (Control_Game.mouse.state == CMouseState.Irrigation)
        {
            // ���
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[3]);
        }
        else if (Control_Game.mouse.state == CMouseState.Fertilizer)
        {
            // ʩ��
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[4]);
        }
        else if (Control_Game.mouse.state == CMouseState.Weed)
        {
            // �ݺ�
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[5]);
        }
        else if (Control_Game.mouse.state == CMouseState.Pet)
        {
            // �溦
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[6]);
        }
        else if (Control_Game.mouse.state == CMouseState.Disease)
        {
            // ����
            Graphics.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 40, 30), texButton[7]);
        }
    }

}
