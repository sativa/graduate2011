using UnityEngine;
using System.Collections;

public class Mod_Pet : MonoBehaviour {

    public GameObject paticle;
    public GameObject[] targetList;
    public float moveSpeed = 2.0f;
    public float pickNextWaypointDistance = 2.0f;
    public float pickNextWaypointRotation = 3.0f;

    GameObject curTarget;   // ��ǰĿ��
    int index;

    bool isShowPetTip;
	// Use this for initialization
	void Start () {
        curTarget = targetList[0];
        index = 0;
	}

    // Update is called once per frame
    void Update()
    {
        MoveTo(curTarget);

        // ���߽�ʱ����ת
        if (pickNextWaypointDistance > Vector3.Magnitude(transform.position - curTarget.transform.position))
        {
            RotateTo(curTarget);

            // ����ת��λʱ��ָ����һ��Ŀ��(Index +1)
            if (pickNextWaypointRotation > Vector3.Magnitude(transform.eulerAngles - curTarget.transform.eulerAngles))
            {
                int next = Random.Range(0, targetList.Length);

                if(next == index)
                    return;

                index= next;
                curTarget = targetList[index];

            }
        }


    }

    // �Զ��ƶ���target
    void MoveTo(GameObject target)
    {
        Vector3 dir = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * moveSpeed);
        transform.position = dir;
    }

    // �Զ��ƶ���target����һ��
    void RotateTo(GameObject target)
    {
        Vector3 dir = Vector3.Lerp(transform.eulerAngles, target.transform.eulerAngles, Time.deltaTime * moveSpeed);
        transform.eulerAngles = dir;
    }

    void OnMouseDown()
    {
        CGameInfo.GetInstance().AddInfo("�����������");

        GameObject.Instantiate(paticle, transform.position+new Vector3(0,1,0), transform.rotation);
    }

    // ���������ʱ����ʾ����Tip
    void OnMouseOver()
    {
        // ��ֹ���η������󣬶�����ϵͳ����
        // �����״̬ΪNoneʱ����ʾ
        if (!isShowPetTip && Control_Game.mouse.state == CMouseState.None)
        {
            isShowPetTip = true;

            GUI_Tip.GetInstance().OpenTip(0, "����Tip");
        }

    }

    // ����뿪ʱ������״̬
    void OnMouseExit()
    {
        isShowPetTip = false;

        GUI_Tip.GetInstance().CloseTip();
    }
}
