using System.Collections;
using System.Collections.Generic;
using TimerCount;
using UnityEngine;
using Zenject;

namespace GameCore
{
    public class Goal : MonoBehaviour
    {
        PopUpController popUpController;
        private void Start()
        {
            TryGetComponent(out popUpController);
            StartCoroutine("PopUpStartStageNumberWindoCoroutine");
        }
        private void OnTriggerEnter(Collider collision)
        {
            // �v���C���[�̃C���^�[�t�F�[�X�擾
            IGoalable hitPlayer = collision.gameObject.GetComponent<IGoalable>();
            // �v���C���[�ɓ���������
            if (hitPlayer != null)
            {
                popUpController.PopUpResultWindoController();
                hitPlayer.GoalAction();
                //TimerController.instance.EndTimer();
                //Debug.Log("�v���C���[��������܂����B");
            }
        }
        IEnumerator PopUpStartStageNumberWindoCoroutine() 
        {
            yield return new WaitForSeconds(2f);
            popUpController.PopUpStartStageNumberWindoController();
        }
    }
}