using UnityEngine;
using System.Collections;

// U3D中的模型，农场的处理
public class Mod_Farm: MonoBehaviour {

    public CFarm farm;
    public bool isAssart;   // 便于测试
    public GameObject maizePerfab;  // 玉米模型
    public GameObject carPerfab;      // 机车模型
    public GameObject carPos;               // 机车模型创建的位置
    public GameObject[] carWayPoints;   //  机车模型的路径

    private GameObject objGUI;
    private bool isShowFarmTip = false;   // 标记是否打开农田GUI
	// Use this for initialization
	void Start () {
        farm = new CFarm(transform.name);
        if (isAssart)
            farm.Assart();

        objGUI = GameObject.Find("Game_GUI");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {

    }

    void OnMouseDown()
    {
        // 粒子效果
        ActiveParticle();
        Invoke("DeactiveParticle", 1);

        objGUI.GetComponent<GUI_GameInfo>().OpenFarmInfoGUI(farm);
 
        
        // 依据不同的鼠标状态产生相应的反应
        // 按照按钮的顺序定义 Assart Seminate Reap Irrigation Fertilizer Weed Pet Disease

        if (Control_Game.mouse.state == CMouseState.Assart)
        {
            // 操作失败的处理
            if (!farm.Assart())
            {
                Control_Game.mouse.state = CMouseState.None;
                return;
            }

            // 创建机车模型
            GameObject obj = (GameObject)GameObject.Instantiate(carPerfab, carPos.transform.position, carPerfab.transform.rotation);
            obj.transform.parent = transform;
            obj.GetComponent<Mod_Car>().targetList = carWayPoints;
        }
        else if (Control_Game.mouse.state == CMouseState.Seminate)
        {
            // 操作失败的处理
            if (!farm.Seminate((CSeed)Control_Game.mouse.obj))
            {
                Control_Game.mouse.state = CMouseState.None;
                return;
            }

            // 创建机车模型
            GameObject carObj = (GameObject)GameObject.Instantiate(carPerfab, carPos.transform.position, carPerfab.transform.rotation);
            carObj.transform.parent = transform;
            carObj.GetComponent<Mod_Car>().targetList = carWayPoints;

            // 创建玉米模型
            GameObject obj = (GameObject)GameObject.Instantiate(maizePerfab, transform.position, transform.rotation);
            obj.transform.parent = transform;
            obj.name="玉米";
        }
        else if (Control_Game.mouse.state == CMouseState.Reap)
        {
            // 操作失败的处理
            if (!farm.Reap())
            {
                Control_Game.mouse.state = CMouseState.None;
                return;
            }

            // 创建机车模型
            GameObject carObj = (GameObject)GameObject.Instantiate(carPerfab, carPos.transform.position, carPerfab.transform.rotation);
            carObj.transform.parent = transform;
            carObj.GetComponent<Mod_Car>().targetList = carWayPoints;

            // 删除玉米模型
            Destroy(GameObject.Find(name+"/玉米"));
        }
        else if (Control_Game.mouse.state == CMouseState.Irrigation)
        {
            // TODO: 弹出灌溉选择窗口
            farm.Irrigation(10);
        }
        else if (Control_Game.mouse.state == CMouseState.Fertilizer)
        {
            // TODO: 弹出肥料选择窗口
            farm.Fertilizer(2, 1, 3);  
        }
        else if (Control_Game.mouse.state == CMouseState.Weed)
        {
            // TODO: 除草
            ;
        }
        else if (Control_Game.mouse.state == CMouseState.Pet)
        {
            // TODO: 除虫
            ;
        }
        else if (Control_Game.mouse.state == CMouseState.Disease)
        {
            // TODO: 治病
            ;
        }

        Control_Game.mouse.state = CMouseState.None;
    }



    // 鼠标在其上时，显示农田Tip
    void OnMouseOver()
    {
        // 防止因多次发送请求，而降低系统性能
        // 当鼠标状态为None时才显示
        if (!isShowFarmTip && Control_Game.mouse.state == CMouseState.None)
        {
            isShowFarmTip = true;

            if (farm != null && farm.crop != null)
                GUI_Tip.GetInstance().OpenTip(99, farm);
            else
                GUI_Tip.GetInstance().OpenTip(0, "尚未播种的农田");
        }

    }

    // 鼠标离开时，重置状态
    void OnMouseExit()
    {
        isShowFarmTip = false;

        GUI_Tip.GetInstance().CloseTip();
    }

    void ActiveParticle()
    {
        GetComponentInChildren<ParticleEmitter>().emit = true;
        GetComponentInChildren<ParticleRenderer>().enabled = true;
    }

    void DeactiveParticle()
    {
        GetComponentInChildren<ParticleEmitter>().emit = false;
        GetComponentInChildren<ParticleRenderer>().enabled = false;
    }
}
