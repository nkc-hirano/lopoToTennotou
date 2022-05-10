using System;
using UnityEngine;

namespace Test_Trap.Pit
{
    public class PitController : MonoBehaviour
    {
        [SerializeField] string charaName;      // �v���C���I�u�W�F�N�g��

        [SerializeField] Vector2 pit_offset;    // �I�u�W�F�N�g�T�C�Y�n�[�t

        Vector3 pos_o;
        GameObject charaObj;
        bool chek;
        event Func<int> testEvent;

        private int TestEvent()
        {
            Debug.Log("�C�x���g");
            return 0;
        }

        TrapNumData CreateTraNumData(Vector3 pos, Vector2 offset)
        {
            TrapNumData data = new TrapNumData();
            data.up = pos.z + offset.y;
            data.down = pos.z - offset.y;
            data.right = pos.x + offset.x;
            data.left = pos.x - offset.x;
            return data;
        }

        bool CheckCharacter(Vector3 pos, TrapNumData data)
        {
            return pos.x >= data.left &&   // X���W�����f�[�^���傫��
                    pos.x <= data.right &&  // X���W���E�f�[�^��菬����
                    pos.z >= data.down &&   // Z���W�����f�[�^���傫��
                    pos.z <= data.up ?      // Z���W����f�[�^��菬����
                    true : false;           // �ꍇtrue
        }
        void Start()
        {
            charaObj = GameObject.Find(charaName);
            pos_o = gameObject.transform.position;
            testEvent += TestEvent;
        }
        void Update()
        {
            Vector3 pos_p = charaObj.transform.position;
            TrapNumData data_o = CreateTraNumData(pos_o, pit_offset);
            if (CheckCharacter(pos_p, data_o) && !chek)
            {
                chek = true;
                Debug.Log("�A�j���[�V����");
                testEvent();
            }

        }
        private void OnDestroy()
        {
            testEvent -= TestEvent;
        }
    }
}