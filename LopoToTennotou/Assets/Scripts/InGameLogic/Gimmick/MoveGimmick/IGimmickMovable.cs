using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public interface IGimmickMovable
    {
        void AddPower(Direction dir, int rightPower, int leftPower);
    }
}
