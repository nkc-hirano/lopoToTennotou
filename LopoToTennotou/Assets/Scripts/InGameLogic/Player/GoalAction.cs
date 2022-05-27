using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

namespace GameCore
{
    public class GoalAction : MonoBehaviour, IGoalable
    {
        [Inject]
        CoreStateController stateController;
        PlayerCore core = null;

        void Start()
        {
            TryGetComponent(out core);
        }

        void IGoalable.GoalAction()
        {
            // �S�[����������
            Debug.Log("�S�[��������B");
            stateController.CoreStateTypeUpdateObserver.OnNext(CoreStateType.Goal);
            stateController.CoreStateTypeUpdateObserver.OnNext(CoreStateType.Final);
            core.PlayerStateUpdate(PlayerStateType.Stop);
        }
    }
}
