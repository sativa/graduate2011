using UnityEngine;
using System.Collections;

// 实现视角的切换
public class GUI_ViewChange : MonoBehaviour
{
    public GameObject firstPersonPerfab;  // 第一人称视角
    public GameObject thirdPersonPerfab; // 第三人称视角

    bool isThirdToFirst = true; // 默认第三视角需要转换到第一视角
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        //transform.Translate(0, -0.1f, 0, Space.World);

        if (isThirdToFirst)
        {
            this.GetComponentInChildren<GUIText>().text = "第三视角";
            // 切换至第一视角，视角建立在原视角对应的xoz平面的位置
            GameObject obj = GameObject.Find("Third Person");
            Object newObj = GameObject.Instantiate(firstPersonPerfab, new Vector3(obj.transform.position.x, firstPersonPerfab.transform.position.y,obj.transform.position.z), firstPersonPerfab.transform.rotation);
            newObj.name = "First Person";

            Destroy(obj);

        }
        else
        {
            this.GetComponentInChildren<GUIText>().text = "第一视角";
            // 切换至第三视角，视角建立在原视角对应的xoz平面的位置
            GameObject obj = GameObject.Find("First Person");
            Object newObj = GameObject.Instantiate(thirdPersonPerfab, new Vector3(obj.transform.position.x, thirdPersonPerfab.transform.position.y, obj.transform.position.z), thirdPersonPerfab.transform.rotation);
            newObj.name = "Third Person";

            Destroy(obj);
        }
        //Debug.Log("123");

        isThirdToFirst = !isThirdToFirst;

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
        GUI_Tip.GetInstance().OpenTip(0, "切换视角");
    }

}
