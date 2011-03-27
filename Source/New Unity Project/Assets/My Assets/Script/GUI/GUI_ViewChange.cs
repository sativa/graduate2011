using UnityEngine;
using System.Collections;

// ʵ���ӽǵ��л�
public class GUI_ViewChange : MonoBehaviour
{
    public GameObject firstPersonPerfab;  // ��һ�˳��ӽ�
    public GameObject thirdPersonPerfab; // �����˳��ӽ�

    bool isThirdToFirst = true; // Ĭ�ϵ����ӽ���Ҫת������һ�ӽ�
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
            this.GetComponentInChildren<GUIText>().text = "�����ӽ�";
            // �л�����һ�ӽǣ��ӽǽ�����ԭ�ӽǶ�Ӧ��xozƽ���λ��
            GameObject obj = GameObject.Find("Third Person");
            Object newObj = GameObject.Instantiate(firstPersonPerfab, new Vector3(obj.transform.position.x, firstPersonPerfab.transform.position.y,obj.transform.position.z), firstPersonPerfab.transform.rotation);
            newObj.name = "First Person";

            Destroy(obj);

        }
        else
        {
            this.GetComponentInChildren<GUIText>().text = "��һ�ӽ�";
            // �л��������ӽǣ��ӽǽ�����ԭ�ӽǶ�Ӧ��xozƽ���λ��
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
        GUI_Tip.GetInstance().OpenTip(0, "�л��ӽ�");
    }

}
