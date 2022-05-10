using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace GameCore
{
    public class GimmickHorizontalMove : MonoBehaviour, IGimmickMovable
    {
        MoveGimmickCore core = null;

        public void AddPower(Direction dir, int rightPower, int leftPower)
        {
            if (rightPower > 255) { Debug.LogWarning("数値がオーバーフローします"); }
            if (leftPower > 255) { Debug.LogWarning("数値がオーバーフローします"); }
            core.GimmickMove(dir, (byte)rightPower, (byte)leftPower);
        }

        void Start()
        {
            TryGetComponent(out core);

            //ここから
            core.CoordObservable.Subscribe();
            core.DirObservable.Subscribe();
        }
    }
}
