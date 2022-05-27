using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

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
            // �S�[����������
            Debug.Log("�S�[��������B");
            core.PlayerStateUpdate(PlayerStateType.Stop);
        }
    }
}