using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(menuName = "StageAssetScriptableObject")]
    public class StageAssetScriptableObject : ScriptableObject
    {
        [SerializeField]
        List<DicPair> dicPairs;

        public GameObject GetStageAssetObject(string name)
        {
            for (int i = 0; i < dicPairs.Count; i++)
            {
                if (dicPairs[i].name == name) { return dicPairs[i].obj; }
            }
            return null;
        }
    }

    [System.Serializable]
    public struct DicPair
    {
        public string name;
        public GameObject obj;
    }
}
