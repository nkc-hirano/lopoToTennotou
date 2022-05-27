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
            // プレイヤーのインターフェース取得
            IGoalable hitPlayer = collision.gameObject.GetComponent<IGoalable>();
            // プレイヤーに当たったら
            if (hitPlayer != null)
            {
                hitPlayer.GoalAction();
                //TimerController.instance.EndTimer();
                //Debug.Log("プレイヤーが当たりました。");
            }
        }
    }
}
