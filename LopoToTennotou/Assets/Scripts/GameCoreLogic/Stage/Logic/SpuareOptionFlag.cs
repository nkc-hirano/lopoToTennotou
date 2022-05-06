using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public enum SpuareOptionFlag
    {
        // Ç»ÇÒÇ©ëùÇ¶ÇÈ
        PitTrap = 1,
        Dark = 2,
        Start = 4,
        Goal = 8,
        Indestructible = 16,
        // 32
        // 64
        // 128
        // 256
        WallUp = 512,
        WallDown = 1024,
        WallLeft = 2048,
        WallRight = 4096,
        WallLeftUp = 8192,
        WallRightDown = 16384,
        WallRightUp  = 32768,
        WallLeftDown = 65536,
    }
}

