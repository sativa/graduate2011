

public class Control_Game 
{
    // Model
    // Pet
    static public int PetLife=3;

    // GUI
    // GUI_GameShop
    static public bool bShowGameShop=false;
    // GUI_GameShop_Buy
    static public bool bShowGameBuyItem = false;
    // GUI_CropPredict
    static public bool bShowCropPredict = false;

    // GUI_GameDetail
    static public bool bGameDetailMove=false;            //  �����Ƿ��ƶ�
    static public bool bGameDetailMoveRight=false;    //  ���������ƶ��������
    // Control
    // Mouse
    public static CMouse mouse=new CMouse();    // ���
    public static bool bMouseDrag = true;             // ��Ӧ�����ק���ƶ����
    // HUD
    public static bool bHUD = false;      // ����Ƿ�����ƶ���HUD�ϣ����Ϊtrue,����Ӧ��������(�����ק�ƶ���ũ�������)

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
