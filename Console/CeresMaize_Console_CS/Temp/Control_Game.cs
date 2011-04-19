

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
    static public bool bGameDetailMove=false;            //  详情是否移动
    static public bool bGameDetailMoveRight=false;    //  详情向哪移动，左或右
    // Control
    // Mouse
    public static CMouse mouse=new CMouse();    // 鼠标
    public static bool bMouseDrag = true;             // 响应鼠标拖拽，移动相机
    // HUD
    public static bool bHUD = false;      // 标记是否鼠标移动在HUD上，如果为true,不响应其他操作(相机拖拽移动、农田左键等)

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
