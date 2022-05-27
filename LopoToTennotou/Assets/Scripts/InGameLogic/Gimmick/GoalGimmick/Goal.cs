using System.Collections;
using System.Collections.Generic;
using TimerCount;
using UnityEngine;
using Zenject;

namespace GameCore
{
    public class Goal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collision)
        {
            // �v���C���[�̃C���^�[�t�F�[�X�擾
            IGoalable hitPlayer = collision.gameObject.GetComponent<IGoalable>();
            // �v���C���[�ɓ���������
            if (hitPlayer != null)
            {
                hitPlayer.GoalAction();
                //TimerController.instance.EndTimer();
                //Debug.Log("�v���C���[��������܂����B");
            }
        }
    }
}
