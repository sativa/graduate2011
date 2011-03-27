using UnityEngine;
using System.Collections;

public class GUI_FarmGUI_Fertilizer : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        //transform.Translate(0, -0.1f, 0, Space.World);
        Control_Game.mouse.state = CMouseState.Fertilizer;
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
        GUI_Tip.GetInstance().OpenTip(0, "Ê©·Ê²Ù×÷");
    }
}
