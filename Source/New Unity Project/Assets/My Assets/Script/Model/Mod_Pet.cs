using UnityEngine;
using System.Collections;

public class Mod_Pet : MonoBehaviour {

    public GameObject paticle;
    public GameObject[] targetList;
    public float moveSpeed = 2.0f;
    public float pickNextWaypointDistance = 2.0f;
    public float pickNextWaypointRotation = 3.0f;

    GameObject curTarget;   // 当前目标
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

        // 当走近时，旋转
        if (pickNextWaypointDistance > Vector3.Magnitude(transform.position - curTarget.transform.position))
        {
            RotateTo(curTarget);

            // 当旋转到位时，指定下一个目标(Index +1)
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

    // 自动移动到target
    void MoveTo(GameObject target)
    {
        Vector3 dir = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * moveSpeed);
        transform.position = dir;
    }

    // 自动移动到target方向一致
    void RotateTo(GameObject target)
    {
        Vector3 dir = Vector3.Lerp(transform.eulerAngles, target.transform.eulerAngles, Time.deltaTime * moveSpeed);
        transform.eulerAngles = dir;
    }

    void OnMouseDown()
    {
        CGameInfo.GetInstance().AddInfo("你击中了田鼠");

        GameObject.Instantiate(paticle, transform.position+new Vector3(0,1,0), transform.rotation);
    }

    // 鼠标在其上时，显示田鼠Tip
    void OnMouseOver()
    {
        // 防止因多次发送请求，而降低系统性能
        // 当鼠标状态为None时才显示
        if (!isShowPetTip && Control_Game.mouse.state == CMouseState.None)
        {
            isShowPetTip = true;

            GUI_Tip.GetInstance().OpenTip(0, "田鼠Tip");
        }

    }

    // 鼠标离开时，重置状态
    void OnMouseExit()
    {
        isShowPetTip = false;

        GUI_Tip.GetInstance().CloseTip();
    }
}
