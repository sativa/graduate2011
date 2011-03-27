using UnityEngine;
using System.Collections;

// U3D�е�ģ�ͣ�ũ���Ĵ���
public class Mod_Farm: MonoBehaviour {

    public CFarm farm;
    public bool isAssart;   // ���ڲ���
    public GameObject maizePerfab;  // ����ģ��
    public GameObject carPerfab;      // ����ģ��
    public GameObject carPos;               // ����ģ�ʹ�����λ��
    public GameObject[] carWayPoints;   //  ����ģ�͵�·��

    private GameObject objGUI;
    private bool isShowFarmTip = false;   // ����Ƿ��ũ��GUI
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
        // ����Ч��
        ActiveParticle();
        Invoke("DeactiveParticle", 1);

        objGUI.GetComponent<GUI_GameInfo>().OpenFarmInfoGUI(farm);
 
        
        // ���ݲ�ͬ�����״̬������Ӧ�ķ�Ӧ
        // ���հ�ť��˳���� Assart Seminate Reap Irrigation Fertilizer Weed Pet Disease

        if (Control_Game.mouse.state == CMouseState.Assart)
        {
            // ����ʧ�ܵĴ���
            if (!farm.Assart())
            {
                Control_Game.mouse.state = CMouseState.None;
                return;
            }

            // ��������ģ��
            GameObject obj = (GameObject)GameObject.Instantiate(carPerfab, carPos.transform.position, carPerfab.transform.rotation);
            obj.transform.parent = transform;
            obj.GetComponent<Mod_Car>().targetList = carWayPoints;
        }
        else if (Control_Game.mouse.state == CMouseState.Seminate)
        {
            // ����ʧ�ܵĴ���
            if (!farm.Seminate((CSeed)Control_Game.mouse.obj))
            {
                Control_Game.mouse.state = CMouseState.None;
                return;
            }

            // ��������ģ��
            GameObject carObj = (GameObject)GameObject.Instantiate(carPerfab, carPos.transform.position, carPerfab.transform.rotation);
            carObj.transform.parent = transform;
            carObj.GetComponent<Mod_Car>().targetList = carWayPoints;

            // ��������ģ��
            GameObject obj = (GameObject)GameObject.Instantiate(maizePerfab, transform.position, transform.rotation);
            obj.transform.parent = transform;
            obj.name="����";
        }
        else if (Control_Game.mouse.state == CMouseState.Reap)
        {
            // ����ʧ�ܵĴ���
            if (!farm.Reap())
            {
                Control_Game.mouse.state = CMouseState.None;
                return;
            }

            // ��������ģ��
            GameObject carObj = (GameObject)GameObject.Instantiate(carPerfab, carPos.transform.position, carPerfab.transform.rotation);
            carObj.transform.parent = transform;
            carObj.GetComponent<Mod_Car>().targetList = carWayPoints;

            // ɾ������ģ��
            Destroy(GameObject.Find(name+"/����"));
        }
        else if (Control_Game.mouse.state == CMouseState.Irrigation)
        {
            // TODO: �������ѡ�񴰿�
            farm.Irrigation(10);
        }
        else if (Control_Game.mouse.state == CMouseState.Fertilizer)
        {
            // TODO: ��������ѡ�񴰿�
            farm.Fertilizer(2, 1, 3);  
        }
        else if (Control_Game.mouse.state == CMouseState.Weed)
        {
            // TODO: ����
            ;
        }
        else if (Control_Game.mouse.state == CMouseState.Pet)
        {
            // TODO: ����
            ;
        }
        else if (Control_Game.mouse.state == CMouseState.Disease)
        {
            // TODO: �β�
            ;
        }

        Control_Game.mouse.state = CMouseState.None;
    }



    // ���������ʱ����ʾũ��Tip
    void OnMouseOver()
    {
        // ��ֹ���η������󣬶�����ϵͳ����
        // �����״̬ΪNoneʱ����ʾ
        if (!isShowFarmTip && Control_Game.mouse.state == CMouseState.None)
        {
            isShowFarmTip = true;

            if (farm != null && farm.crop != null)
                GUI_Tip.GetInstance().OpenTip(99, farm);
            else
                GUI_Tip.GetInstance().OpenTip(0, "��δ���ֵ�ũ��");
        }

    }

    // ����뿪ʱ������״̬
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
