using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    // シングルトンにする
    // ロジックシーンにアタッチする
    public class StageDataContainer : MonoBehaviour
    {
        private static StageDataContainer instance = null;

        int playerClearStageNum = 0;

        [SerializeField]
        string[] stageDataFileNames;
        [SerializeField]
        StageAssetScriptableObject assetScriptableObject;
        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        void Update()
        {

        }

        public static string GetStageDataFileName(int stageNum)
        {
            if (stageNum > instance.stageDataFileNames.Length) { return null; }

            return instance.stageDataFileNames[stageNum - 1];
        }

        public static void StageClearInfoRegister(int stageNum)
        {
            instance.playerClearStageNum = Mathf.Max(1, stageNum);
        }

        public GameObject GetStageAssetGameObject(string name)
        {
            return assetScriptableObject.GetStageAssetObject(name);
        }
    }
}
