using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameCore
{
    public class StageInstantiater : StageDataLoaderBase
    {
        [SerializeField]
        StageAssetScriptableObject stageAsset;
        [SerializeField, Tooltip("0��tutorial")]
        List<string> stageDataFileNameList = new List<string>();

        [SerializeField]
        NameStageObjPair[] objPairs;
        [SerializeField]
        string stdData;

        static Dictionary<string, XYPOS> posDic;

        Direction[,] moveGimmickData = null;
        Direction[,] buttonGimmickData = null;
        SpuareOptionFlag[,] otherGimmickData = null;

        GameObject gimmckObjMother;

        void Start()
        {
            posDic = new Dictionary<string, XYPOS>();
            //StageCreate();
        }

        public GameObject RootObject => gimmckObjMother;

        public void StageCreate(int stageNum)
        {
            if (stageDataFileNameList.Count < stageNum)
            {
                Debug.Log($"�����ȃG���[");
                return;
            }
            // �t�@�C����ǂݍ��݃e�L�X�g�ϊ�
            TextAsset txtData = (TextAsset)Resources.Load(stageDataFileNameList[stageNum]);
            // json�t�@�C���Ƃ��ĕϊ�����StageDataFileFormat�^�ɕϊ�
            var jsonData = JsonUtility.FromJson<StageDataFileFormat>(txtData.text);
            // �I�u�W�F�N�g�̈ʒu�����擾
            jsonData.GetParallelData(out moveGimmickData, out buttonGimmickData, out otherGimmickData);

            // �I�u�W�F�N�g�̈ʒu����o�^?
            initializer = gameObject.AddComponent<StageDataController>();
            initializer.StageDataRegister(moveGimmickData, buttonGimmickData, otherGimmickData);

            // �I�u�W�F�N�g�̐���
            CreateObject(jsonData);
        }

        void CreateObject(StageDataFileFormat data)
        {
            gimmckObjMother = new GameObject("Root");
            for (int i = 0; i < data.xLength; i++)
            {
                for (int j = 0; j < data.yLength; j++)
                {
                    if (!moveGimmickData[i, j].HasFlag(Direction.Zero))
                    {
                        MoveGimmickCreate(i, j, data);
                    }
                    else if (!buttonGimmickData[i, j].HasFlag(Direction.Zero))
                    {
                        ButtonGimmickCreate(i, j, data);
                    }
                    else if (otherGimmickData[i, j] != 0)
                    {
                        SpuareOptionCreate(i, j, data);
                    }
                }
            }
        }

        void MoveGimmickCreate(int i, int j, StageDataFileFormat data)
        {
            Vector3 dir = Vector3.zero;
            GimmickObjNumber number = GimmickObjNumber.MoveObj;
            if (moveGimmickData[i, j].HasFlag(Direction.Up)) { dir = new Vector3(0, 180); }
            else if (moveGimmickData[i, j].HasFlag(Direction.Down)) { dir = Vector3.zero; }
            else if (moveGimmickData[i, j].HasFlag(Direction.Right)) { dir = new Vector3(0, -90); }
            else if (moveGimmickData[i, j].HasFlag(Direction.Left)) { dir = new Vector3(0, 90); }
            GimmickCreate(number, dir, i, j, data);
        }
        void ButtonGimmickCreate(int i, int j, StageDataFileFormat data)
        {
            Vector3 dir = Vector3.zero;
            GimmickObjNumber number = GimmickObjNumber.ButtonObj;
            // ���S�{�^��
            if (buttonGimmickData[i, j].HasFlag(Direction.Up) &&
                buttonGimmickData[i, j].HasFlag(Direction.Down) &&
                buttonGimmickData[i, j].HasFlag(Direction.Right) &&
                buttonGimmickData[i, j].HasFlag(Direction.Left))
            {
                number = GimmickObjNumber.ButtonObj2;
                dir = Vector3.zero;
            }
            // ���{�^��
            else if (buttonGimmickData[i, j].HasFlag(Direction.Up))
            {
                dir = new Vector3(0, 180);
            }
            else if (buttonGimmickData[i, j].HasFlag(Direction.Down))
            {
                dir = Vector3.zero;
            }
            else if (buttonGimmickData[i, j].HasFlag(Direction.Right))
            {
                dir = new Vector3(0, -90);
            }
            else if (buttonGimmickData[i, j].HasFlag(Direction.Left))
            {
                dir = new Vector3(0, 90);
            }
            GimmickCreate(number, dir, i, j, data);
        }
        void SpuareOptionCreate(int i, int j, StageDataFileFormat data)
        {
            Vector3 dir = Vector3.zero;
            GimmickObjNumber number = GimmickObjNumber.Start;

            // �X�^�[�g
            if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Start)) { number = GimmickObjNumber.Start; }
            // �S�[��
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Goal)) { number = GimmickObjNumber.Goal; }
            // �s���~�܂�
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Indestructible)) { number = GimmickObjNumber.Indestructible; }

            // ��
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallUp))
            {
                number = GimmickObjNumber.Wall;
                //dir = Vector3.zero;
                dir = new Vector3(0, -90);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallDown))
            {
                number = GimmickObjNumber.Wall;
                //dir = new Vector3(0, 180);
                dir = new Vector3(0, 90);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallLeft))
            {
                number = GimmickObjNumber.Wall;
                //dir = new Vector3(0, 90);
                dir = new Vector3(0, 180);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallRight))
            {
                number = GimmickObjNumber.Wall;
                //dir = new Vector3(0, -90);
                dir = Vector3.zero;
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallLeftUp))
            {
                number = GimmickObjNumber.WallSide;
                dir = Vector3.zero;
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallRightDown))
            {
                number = GimmickObjNumber.WallSide;
                dir = new Vector3(0, 180);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallRightUp))
            {
                number = GimmickObjNumber.WallSide;
                dir = new Vector3(0, 90);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallLeftDown))
            {
                number = GimmickObjNumber.WallSide;
                dir = new Vector3(0, -90);
            }

            // ��C
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.CannonTrapUp))
            {
                number = GimmickObjNumber.CannonTrapObj;
                dir = Vector3.zero;
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.CannonTrapDown))
            {
                number = GimmickObjNumber.CannonTrapObj;
                dir = new Vector3(0, 180);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.CannonTrapLeft))
            {
                number = GimmickObjNumber.CannonTrapObj;
                dir = new Vector3(0, -90);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.CannonTrapRight))
            {
                number = GimmickObjNumber.CannonTrapObj;
                dir = new Vector3(0, 90);
            }
            // ���Ƃ���
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.PitTrap)) { number = GimmickObjNumber.PitTrap; }
            // �È�
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Dark)) { number = GimmickObjNumber.Dark; }
            // ����
            GimmickCreate(number, dir, i, j, data);
        }

        void GimmickCreate(GimmickObjNumber nuber, Vector3 dir, int i, int j, StageDataFileFormat data)
        {

            float offset = (data.xLength - 1) / 2;
            GameObject gimmckObj = Instantiate(objPairs[(int)nuber].obj);
            GameObject stageFloorObj = Instantiate(objPairs[objPairs.Length-1].obj);

            gimmckObj.name = i + "_" + j;
            posDic[gameObject.name] = new XYPOS((byte)i, (byte)j, data.xLength);
            gimmckObj.transform.eulerAngles = dir;
            gimmckObj.transform.localPosition = new Vector3(j - offset, 0, -i + offset);
            stageFloorObj.transform.localPosition = new Vector3(j - offset, -1, -i + offset);
            gimmckObj.transform.parent = gimmckObjMother.transform;
        }

        public void Hoge(string name,out byte x,out byte y,out int mapScale)
        {
            
            if (posDic.ContainsKey(gameObject.name))
            {
                Debug.Log("Gagagagagagagag");
                x = posDic[gameObject.name].x;
                y = posDic[gameObject.name].y;
                mapScale = posDic[gameObject.name].mapScale;
            }
            else
            {
                x = default;
                y = default;
                mapScale = default;
            }
        }
    }
}