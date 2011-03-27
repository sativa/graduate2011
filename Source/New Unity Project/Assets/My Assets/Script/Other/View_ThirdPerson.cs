using UnityEngine;
using System.Collections;

// 使用滚轴可以改变相机的高度
public class View_ThirdPerson : MonoBehaviour {

    public float camMoveSpeed = 5;
    public float camSizeNear = 15;
    public float camSizeFar = 60;
    
    Vector3 mousePos;
    Vector3 deltaVec;
    bool bMouseMove;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
        float f = Input.GetAxis("Mouse ScrollWheel");
        if (f != 0)
        {
            //Debug.Log(f);

            /*
            if (transform.position.y <= 10)   //  接近地面时，不响应
                transform.Translate(0, 0.1f, 0,Space.World);  
            else 
            transform.Translate(0, -f*5, 0);
             * */

            GetComponentInChildren<Camera>().orthographicSize += f*5;
        }
        
        
        // 鼠标按下时开始移动
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            bMouseMove = true;
       }

        // 鼠标松开时，结束移动
        if (Input.GetMouseButtonUp(0))
        {
            bMouseMove = false;
        }

        // 每次移动，计算新的移动距离
        if (bMouseMove)
        {
            deltaVec = Input.mousePosition - mousePos;
            transform.Translate(-deltaVec.x * Time.deltaTime * camMoveSpeed, 0, -deltaVec.y * Time.deltaTime * camMoveSpeed);
            mousePos = Input.mousePosition;
        }
	}

}
