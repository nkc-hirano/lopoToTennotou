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
        [SerializeField, Tooltip("0はtutorial")]
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
                Debug.Log($"無効なエラー");
                return;
            }
            // ファイルを読み込みテキスト変換
            TextAsset txtData = (TextAsset)Resources.Load(stageDataFileNameList[stageNum]);
            // jsonファイルとして変換してStageDataFileFormat型に変換
            var jsonData = JsonUtility.FromJson<StageDataFileFormat>(txtData.text);
            // オブジェクトの位置情報を取得
            jsonData.GetParallelData(out moveGimmickData, out buttonGimmickData, out otherGimmickData);

            // オブジェクトの位置情報を登録?
            initializer = gameObject.AddComponent<StageDataController>();
            initializer.StageDataRegister(moveGimmickData, buttonGimmickData, otherGimmickData);

            // オブジェクトの生成
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
            // 中心ボタン
            if (buttonGimmickData[i, j].HasFlag(Direction.Up) &&
                buttonGimmickData[i, j].HasFlag(Direction.Down) &&
                buttonGimmickData[i, j].HasFlag(Direction.Right) &&
                buttonGimmickData[i, j].HasFlag(Direction.Left))
            {
                number = GimmickObjNumber.ButtonObj2;
                dir = Vector3.zero;
            }
            // 矢印ボタン
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

            // スタート
            if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Start)) { number = GimmickObjNumber.Start; }
            // ゴール
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Goal)) { number = GimmickObjNumber.Goal; }
            // 行き止まり
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Indestructible)) { number = GimmickObjNumber.Indestructible; }

            // 壁
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

            // 大砲
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
            // 落とし穴
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.PitTrap)) { number = GimmickObjNumber.PitTrap; }
            // 暗闇
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Dark)) { number = GimmickObjNumber.Dark; }
            // 生成
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