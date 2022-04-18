using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameInitialize
{
    public class FirstSceneLoader : MonoBehaviour
    {
        [SerializeField]
        string FirstSceneName;
        [SerializeField]
        bool isStartLoad = false;

        void Start()
        {
            if (!isStartLoad) { return; }
            LoadFirstSceneAsSingle();
        }

        public void LoadFirstSceneAsSingle()
        {
            SceneManager.LoadSceneAsync(FirstSceneName, LoadSceneMode.Single);
        }
    }
}
