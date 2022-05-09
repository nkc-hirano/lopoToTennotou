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
        GameObject[] gimmickObj;
        [SerializeField]
        NameStageObjPair[] objPairs;
        [SerializeField]
        string stdData;

        Direction[,] moveGimmickData = null;
        Direction[,] buttonGimmickData = null;
        SpuareOptionFlag[,] otherGimmickData = null;

        void Start()
        {
            // StageDataFileFormatを取得
            // ↓登録
            // initializer.StageDataRegister();
            // 生成 メソッドにしておく
            // ※後で変数名は変更する！！

            // ファイルを読み込みテキスト変換
            TextAsset txtData = (TextAsset)Resources.Load(stdData);
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
            for (int i = 0; i < data.xLength; i++)
            {
                for (int j = 0; j < data.yLength; j++)
                {
                    if (!moveGimmickData[i, j].HasFlag(Direction.Zero))
                    {
                        MoveGimmickCreate(data, i, j);
                    }
                    else if (!buttonGimmickData[i, j].HasFlag(Direction.Zero))
                    {
                        ButtonGimmickCreate(data, i, j);
                    }
                    else if (otherGimmickData[i, j] != 0)
                    {
                        SpuareOptionCreate(data, i, j);
                    }
                }
            }
        }
        void MoveGimmickCreate(StageDataFileFormat data, int i, int j)
        {
            Vector3 dir = Vector3.zero;
            GameObject gimmckObj = Instantiate(gimmickObj[(int)GimmickNumber.MoveObj]);
            if (moveGimmickData[i, j].HasFlag(Direction.Up)) dir = Vector3.zero;
            else if (moveGimmickData[i, j].HasFlag(Direction.Down)) dir = new Vector3(0, 180);
            else if (moveGimmickData[i, j].HasFlag(Direction.Right)) dir = new Vector3(0, 90);
            else if (moveGimmickData[i, j].HasFlag(Direction.Left)) dir = new Vector3(0, -90);
            else Debug.Log(otherGimmickData[i, j]);
            gimmckObj.transform.eulerAngles = dir;
            gimmckObj.transform.localPosition = new Vector3(j, 0, -i);
        }
        void ButtonGimmickCreate(StageDataFileFormat data, int i, int j)
        {
            Vector3 dir = Vector3.zero;
            GameObject gimmckObj ;
            if (buttonGimmickData[i, j].HasFlag(Direction.Up) &&
                buttonGimmickData[i, j].HasFlag(Direction.Down) &&
                buttonGimmickData[i, j].HasFlag(Direction.Right) &&
                buttonGimmickData[i, j].HasFlag(Direction.Left))
            {
                gimmckObj = Instantiate(gimmickObj[(int)GimmickNumber.ButtonObj2]);
                dir = Vector3.zero;
            }
            else if (buttonGimmickData[i, j].HasFlag(Direction.Up))
            {
                gimmckObj = Instantiate(gimmickObj[(int)GimmickNumber.ButtonObj]);
                dir = Vector3.zero;
            }
            else if (buttonGimmickData[i, j].HasFlag(Direction.Down))
            {
                gimmckObj = Instantiate(gimmickObj[(int)GimmickNumber.ButtonObj]);
                dir = new Vector3(0, 180);
            }
            else if (buttonGimmickData[i, j].HasFlag(Direction.Right))
            {
                gimmckObj = Instantiate(gimmickObj[(int)GimmickNumber.ButtonObj]);
                dir = new Vector3(0, 90);
            }
            else if (buttonGimmickData[i, j].HasFlag(Direction.Left))
            {
                gimmckObj = Instantiate(gimmickObj[(int)GimmickNumber.ButtonObj]);
                dir = new Vector3(0, -90);
            }
            else gimmckObj = null;
            gimmckObj.transform.eulerAngles = dir;
            gimmckObj.transform.localPosition = new Vector3(j, 0, -i);
        }
        void SpuareOptionCreate(StageDataFileFormat data, int i, int j)
        {
            Vector3 dir = Vector3.zero;
            GimmickNumber number = new GimmickNumber();
            GameObject gimmckObj;
            if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Start)) number = GimmickNumber.Start;
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Goal)) number = GimmickNumber.Goal;
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Indestructible)) number = GimmickNumber.Indestructible;
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallUp))
            {
                number = GimmickNumber.Wall;
                dir = Vector3.zero;
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallDown))
            {
                number = GimmickNumber.Wall;
                dir = new Vector3(0, 180);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallLeft))
            {
                number = GimmickNumber.Wall;
                dir = new Vector3(0, 90);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallRight))
            {
                number = GimmickNumber.Wall;
                dir = new Vector3(0, -90);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallLeftUp))
            {
                number = GimmickNumber.WallSide;
                dir = Vector3.zero;
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallRightDown))
            {
                number = GimmickNumber.WallSide;
                dir = new Vector3(0, 180);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallRightUp))
            {
                number = GimmickNumber.WallSide;
                dir = new Vector3(0, 90);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.WallLeftDown))
            {
                number = GimmickNumber.WallSide;
                dir = new Vector3(0, -90);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.CannonTrapUp))
            {
                number = GimmickNumber.CannonTrapObj;
                dir = Vector3.zero;
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.CannonTrapDown))
            {
                number = GimmickNumber.CannonTrapObj;
                dir = new Vector3(0, 180);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.CannonTrapLeft))
            {
                number = GimmickNumber.CannonTrapObj;
                dir = new Vector3(0, -90);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.CannonTrapRight))
            {
                number = GimmickNumber.CannonTrapObj;
                dir = new Vector3(0, 90);
            }
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.PitTrap)) number = GimmickNumber.PitTrap;
            else if (otherGimmickData[i, j].HasFlag(SpuareOptionFlag.Dark)) number = GimmickNumber.Dark;
            else Debug.Log(otherGimmickData[i, j]);
            gimmckObj = Instantiate(gimmickObj[(int)number]);
            gimmckObj.transform.eulerAngles = dir;
            gimmckObj.transform.localPosition = new Vector3(j, 0, -i);
        }
    }
    enum GimmickNumber
    {
        Start,
        Goal,
        Indestructible,
        Wall,
        WallSide,
        MoveObj,
        ButtonObj,
        ButtonObj2,
        PitTrap,
        Dark,
        CannonTrapObj,
    }

    [System.Serializable]
    struct NameStageObjPair
    {
        public string name;
        public GameObject obj;
    }
}