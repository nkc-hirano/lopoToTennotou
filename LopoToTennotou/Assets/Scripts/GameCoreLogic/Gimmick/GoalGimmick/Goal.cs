using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                //Debug.Log("プレイヤーが当たりました。");
            }
        }
    }
}
