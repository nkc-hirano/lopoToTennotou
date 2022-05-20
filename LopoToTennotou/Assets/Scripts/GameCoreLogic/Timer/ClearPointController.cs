using System;
using UnityEngine;
using Trap;
using GameCore;
using Zenject;
using UniRx;

namespace TimerCount.Clear
{
    public class ClearPointController : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] Vector2 objOffset;

        [Inject]
        CoreStateController coreStateController;

        // private Subject<int> goulSubject;
        // public IObservable<int> GoulObservable => goulSubject; 

        // public event Func<int> eventSystemHandler;

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
            //goulSubject.Subscribe(var => { TestEvent(); });
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
                TestEvent();
                //eventSystemHandler();
            }
        }
        private void TestEvent()
        {
            Debug.Log("クリア");

            coreStateController.CoreStateTypeUpdateObserver.OnNext(CoreStateType.Goal);
//            return 0;
        }
        private void OnDestroy()
        {
           // eventSystemHandler -= TestEvent;
        }
    }
}