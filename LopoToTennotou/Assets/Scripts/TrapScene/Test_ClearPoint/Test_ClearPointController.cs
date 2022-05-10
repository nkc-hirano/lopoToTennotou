using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test_Trap;

namespace Test_TimerCount.Clear
{
    public class Test_ClearPointController : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] Vector2 objOffset;

        public event Func<int> eventSystemHandler;
        TrapNumData CreateTrapNumData(Vector3 pos,Vector2 offset) 
        {
            TrapNumData trapNumDataBox = new TrapNumData();
            trapNumDataBox.up = pos.z + offset.y;
            trapNumDataBox.down = pos.z - offset.y;
            trapNumDataBox.right = pos.x + offset.x;
            trapNumDataBox.left = pos.x - offset.x;
            return trapNumDataBox;
        }
        bool CheckCharacter(Vector3 pos, TrapNumData data)
        {
            return pos.x >= data.left &&   // X座標が左データより大きく
                    pos.x <= data.right &&  // X座標が右データより小さく
                    pos.z >= data.down &&   // Z座標が下データより大きく
                    pos.z <= data.up ?      // Z座標が上データより小さい
                    true : false;           // 場合true
        }
        private void Start()
        {
            eventSystemHandler += TestEvent;
           
        }
        private void Update()
        {
            // データ作成
            Vector3 pos_P = player.transform.position;
            Vector3 pos_O = gameObject.transform.position;
            TrapNumData oData = CreateTrapNumData(pos_O, objOffset);
            
            // プレイヤがエリア内に入ったことを検知したらイベント発火
            if (CheckCharacter(pos_P, oData))
            {
                eventSystemHandler();
            }
        }
        private int TestEvent()
        {
            Debug.Log("クリア");
            return 0;
        }
        private void OnDestroy()
        {
            eventSystemHandler -= TestEvent;
        }
    }
}