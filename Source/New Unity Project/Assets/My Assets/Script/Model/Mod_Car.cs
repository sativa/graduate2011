using UnityEngine;
using System.Collections;

public class Mod_Car : MonoBehaviour {

    public GameObject[] targetList;
    public float moveSpeed = 2.0f;
    public float pickNextWaypointDistance = 2.0f;
    public float pickNextWaypointRotation = 3.0f;
    int index = 0;
    GameObject curTarget;   // 当前目标
	// Use this for initialization
	void Start () {        
        curTarget = targetList[index];
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
                index++;

                // 走到终点时，销毁
                if (index == targetList.Length)
                {
                    Destroy(this.gameObject);
                    return;
                }

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
}
