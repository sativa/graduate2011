using UnityEngine;
using System.Collections;

public class Mod_Maize : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // ֻ�ܰ󶨵��Ӷ������ϣ������Maize������Ӷ�����
    void OnMouseOver()
    {
        //Debug.Log("Maize Call");
        GUI_Tip.GetInstance().OpenTip(99, transform.parent.parent.GetComponent<Mod_Farm>().farm);
    }
}
