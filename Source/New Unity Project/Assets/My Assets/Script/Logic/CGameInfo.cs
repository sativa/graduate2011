using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


// Singlton
    public class CGameInfo
    {
        static private CGameInfo instance= new CGameInfo();
        private List<string> infoList = new List<string>();

        static public CGameInfo GetInstance()
        {
            return instance;
        }

        // 插入的str应该使用英文标点，如’,‘，而不使用中文标点，如’，‘。原因：中文标点显示出来不美观 
        // 在任何需要的地方都会调用CGameInfo.GetInstance().AddInfo(str)
        public void AddInfo(string str)
        {
            GUI_GameMSG.scrollPosition.y = 10000;
            infoList.Add(str);
        }

        public string GetInfoAt(int i)
        {
            return infoList[i];
        }

        public int GetInfoLength()
        {
            return infoList.Count;
        }
    }

