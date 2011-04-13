using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

// 完成地域信息的读取和创建
namespace CeresMaize_Console_CS
{
    // Singlton
    public class CTerrain
    {
        static private CTerrain instance=new CTerrain();

        public DateTime dt = new DateTime(2011, 1, 1);

        public int[] JDATE = new int[365];
        public float[] TEMPMX = new float[365];
        public float[] TEMPMN = new float[365];
        public float[] RAIN = new float[365];
        public float[] SOLRAD = new float[365];

        static public CTerrain GetInstance()
        {
            return instance;
        }

        public void Init(string fileName)
        {
            ReadTerrainInfo(fileName);
            instance = this;
        }

        void ReadTerrainInfo(string fileName)
        {
            string[] lines = File.ReadAllLines("1.Terrain");
            for (int i = 0; i < 365;i++ )
            {
                string[] sp = lines[i].Split('\t');
                JDATE[i] = Convert.ToInt32(sp[0]);
                TEMPMX[i] = Convert.ToSingle(sp[1]);
                TEMPMN[i] = Convert.ToSingle(sp[2]);
                RAIN[i] = Convert.ToSingle(sp[3]);
                SOLRAD[i] = Convert.ToSingle(sp[4]);
            }
        }
    }

}
