using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public struct InputStructConverterToVecter
    {
        public byte x; // ���̍��Wx
        public byte y; // ���̍��Wy

        public int mapScale; // �}�b�v�̑傫��

        public InputStructConverterToVecter(byte x, byte y, int mapScale)
        {
            this.x = x;
            this.y = y;
            this.mapScale = mapScale;
        }
    }
}
