using UnityEngine;
using System.Collections;

public class GUI_InfoButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        //transform.Translate(0, -0.1f, 0, Space.World);
        Control_Game.bShowGameInfoGUI =!Control_Game.bShowGameInfoGUI;

        if (Control_Game.bShowGameInfoGUI)
        {
            // 此时详情打开，应显示关闭提示
            this.GetComponentInChildren<GUIText>().text = "隐藏详情";
        }
        else
        {
            // 此时详情关闭，应显示打开提示
            this.GetComponentInChildren<GUIText>().text = "显示详情";
        }
        //Debug.Log("123");
    }

    void OnMouseUp()
    {
        //transform.Translate(0, 0.1f, 0, Space.World);

        //Debug.Log("321");

    }

    void OnMouseEnter()
    {
        transform.localScale += new Vector3(0.01f, 0.01f, 0);
    }

    void OnMouseExit()
    {
        transform.localScale -= new Vector3(0.01f, 0.01f, 0);

        GUI_Tip.GetInstance().CloseTip();
    }

    void OnMouseOver()
    {
        GUI_Tip.GetInstance().OpenTip(0, "切换信息");
    }

}
