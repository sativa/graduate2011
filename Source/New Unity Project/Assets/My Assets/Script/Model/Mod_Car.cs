using UnityEngine;
using System.Collections;

public class Mod_Car : MonoBehaviour {

    public GameObject[] targetList;
    public float moveSpeed = 2.0f;
    public float pickNextWaypointDistance = 2.0f;
    public float pickNextWaypointRotation = 3.0f;
    int index = 0;
    GameObject curTarget;   // ��ǰĿ��
	// Use this for initialization
	void Start () {        
        curTarget = targetList[index];
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
                index++;

                // �ߵ��յ�ʱ������
                if (index == targetList.Length)
                {
                    Destroy(this.gameObject);
                    return;
                }

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
}
