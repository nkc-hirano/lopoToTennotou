using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneController
{
    public class DebugStartUpUpdater : MonoBehaviour
    {
        SceneStateUpdaterBase updater;

        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out updater);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                updater.LoadNextScene();
            }
        }
    }
}
