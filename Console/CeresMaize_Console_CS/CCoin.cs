using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace CeresMaize_Console_CS
{


    //singlton
    public class CCoin
    {

        static private CCoin instance = new CCoin();
        private int coin;				//�������
        public int[] seedBuyPrice = new int[100];	//��������۸�
        public int[] seedSalePrice = new int[100];	//ũ���������۸�
        public int[] farmOperation = new int[20];	//ũ�����������۸�

        static public CCoin GetInstance()
        {
            return instance;
        }

        //���캯��
        private CCoin()
        {
            readCoinInfo();
        }

        //����Ǯ�����в���
        public bool processCoin(ECoinState state)
        {
            if (state == ECoinState.None)
                return false;
            else if (processSeed(state))				//����
                return true;
            else if (processFarmOperation(state))	//���ص�ũ������
                return true;
            else if (processSale(state))				//�ջ�
                return true;
            else if (processUpdateFarm(state))		//��������
                return true;
            else		//ûǮ�Ĵ���
            {
                CGameInfo.GetInstance().AddInfo("̫���˰ɣ����Ǯ��û��");
                return false;
            }
        }

        //������
        bool processSeed(ECoinState state)
        {
            //��ʱֻ�����ף��Ժ����˺��̵���ϵ����
            switch (state)
            {
                case ECoinState.Seminate:
                    return addOrMinusMoney(seedBuyPrice[0], false);
                default:
                    return false;
            }
        }

        //���س��ݵȲ������ѽ��
        bool processFarmOperation(ECoinState state)
        {
            switch (state)
            {
                case ECoinState.Assart:
                    return addOrMinusMoney(farmOperation[0], false);
                case ECoinState.Irrigation:
                    return addOrMinusMoney(farmOperation[1], false);
                case ECoinState.Fertilizer:
                    return addOrMinusMoney(farmOperation[2], false);
                case ECoinState.Weed:
                    return addOrMinusMoney(farmOperation[3], false);
                case ECoinState.Pet:
                    return addOrMinusMoney(farmOperation[4], false);
                default:
                    return false;
            }
        }

        //��������
        bool processUpdateFarm(ECoinState state)
        {
            return false;
        }

        //�ջ�ʱ��Ҳ���
        bool processSale(ECoinState state)
        {
            //��ʱֻ������
            switch (state)
            {
                case ECoinState.Reap:
                    return addOrMinusMoney(seedSalePrice[0], true);
                default:
                    return false;
            }
        }

        //��Ǯ�ͼ�Ǯ����������moneyΪǮ����addΪ��Ǯ��־trueΪ��ǮfalseΪ��Ǯ
        bool addOrMinusMoney(int money, bool add)
        {
            if (add)
                coin += money;
            else
            {
                if (coin - money >= 0)	//ȷ��Ǯ����Ϊ����
                    coin -= money;
                else
                    return false;
            }
            return true;
        }

        //��ȡ�����Ϣ�ļ�������û�ļ���д����������
        void readCoinInfo()
        {
            coin = 100;		//��ҳ�ʼ��Ϊ100
            //���ӹ���۸�
            seedBuyPrice[0] = 9;
            //�����۸�
            seedSalePrice[0] = 70;
            //���س��ݵȲ����۸�
            farmOperation[0] = 1;	//����
            farmOperation[1] = 1;	//���
            farmOperation[2] = 1;	//ʩ��
            farmOperation[3] = 1;	//����
            farmOperation[4] = 1;	//���棨С��
            farmOperation[5] = 2;	//���棨�У�
            farmOperation[6] = 3;	//���棨��
            //���������۸񣨴����䣩
        }

        //���ؽ��ֵ
        public int getCoin()
        {
            return coin;
        }
    }

    
};