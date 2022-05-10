using UnityEngine;
using Cinemachine;

namespace ShakeChange
{
    public class ShakeCameraChangeController : MonoBehaviour
    {
        [SerializeField]
        CinemachineVirtualCamera VC1;
        [SerializeField]
        CinemachineVirtualCamera VC2;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                VC1.Priority = VC1.Priority==(int)modeChange.active? (int)modeChange.Inactive : (int)modeChange.active;
                VC2.Priority = VC2.Priority==(int)modeChange.active? (int)modeChange.Inactive : (int)modeChange.active;
            }
        }
    }
    enum modeChange
    {
        Inactive,
        active,
    }
}