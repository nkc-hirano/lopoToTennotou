using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameCore
{
    public class StageInstantiater : MonoBehaviour
    {
        void Start()
        {
            
        }

        public void StageInsatntiate(int stageNum)
        {

        }

        private async UniTask LoadStageData(int stageNum)
        {
            await UniTask.Yield();
        }
    }
}
