using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameCore
{
    public class GoalAction : MonoBehaviour, IGoalable
    {
        PlayerCore core = null;

        void Start()
        {
            TryGetComponent(out core);
        }

        void IGoalable.GoalAction()
        {
            // ÉSÅ[ÉãÇµÇΩèàóù
            //Debug.Log("ÉSÅ[ÉãÇµÇΩÇÊÅB");
            core.PlayerStateUpdate(PlayerStateType.Stop);
        }
    }
}
