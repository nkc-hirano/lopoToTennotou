using GameCore;
using System;
using UniRx;
using UnityEngine;

namespace Trap.Pit
{
    public class PitController : MonoBehaviour
    {
        [SerializeField] string charaName;      // �v���C���I�u�W�F�N�g��

        [SerializeField] Vector2 pit_offset;    // �I�u�W�F�N�g�T�C�Y�n�[�t

        [SerializeField] PlayerCore core;
        [SerializeField] PlayerSus sus;
        
        Vector3 pos_o;
        GameObject charaObj;
        bool chek;

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
        }
        void Update()
        {
            Vector3 pos_p = charaObj.transform.position;

            TrapNumData data_o = CreateTraNumData(pos_o, pit_offset);
            if (CheckCharacter(pos_p, data_o) && !chek)
            {
                chek = true;
                core.PlayerStateUpdate(PlayerStateType.Fall);
                sus.PlayerSusSubject.OnNext(Unit.Default);
            }
        }
    }
}