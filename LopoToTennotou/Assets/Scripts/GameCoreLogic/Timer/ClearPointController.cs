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
            return pos.x >= data.left &&   // X���W�����f�[�^���傫��
                    pos.x <= data.right &&  // X���W���E�f�[�^��菬����
                    pos.z >= data.down &&   // Z���W�����f�[�^���傫��
                    pos.z <= data.up ?      // Z���W����f�[�^��菬����
                    true : false;           // �ꍇtrue
        }
        private void Start()
        {
            //goulSubject.Subscribe(var => { TestEvent(); });
        }
        private void Update()
        {
            // �f�[�^�쐬
            Vector3 pos_P = player.transform.position;
            Vector3 pos_O = gameObject.transform.position;
            TrapNumData oData = CreateTrapNumData(pos_O, objOffset);
            
            // �v���C�����G���A���ɓ��������Ƃ����m������C�x���g����
            if (CheckCharacter(pos_P, oData))
            {
                TestEvent();
                //eventSystemHandler();
            }
        }
        private void TestEvent()
        {
            Debug.Log("�N���A");

            coreStateController.CoreStateTypeUpdateObserver.OnNext(CoreStateType.Goal);
//            return 0;
        }
        private void OnDestroy()
        {
           // eventSystemHandler -= TestEvent;
        }
    }
}