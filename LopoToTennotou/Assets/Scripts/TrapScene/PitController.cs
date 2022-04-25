using System;
using UnityEngine;

public class PitController : MonoBehaviour
{
    [SerializeField] string charaName;      // プレイヤオブジェクト名

    [SerializeField] Vector2 pit_offset;    // オブジェクトサイズハーフ

    Vector3 pos_o;
    GameObject charaObj;
    bool chek;
    event Func<int> testEvent;

    private int TestEvent()
    {
        Debug.Log("イベント");
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
        return  pos.x >= data.left &&   // X座標が左データより大きく
                pos.x <= data.right &&  // X座標が右データより小さく
                pos.z >= data.down &&   // Z座標が下データより大きく
                pos.z <= data.up ?      // Z座標が上データより小さい
                true : false;           // 場合true
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
            Debug.Log("アニメーション");
            testEvent();
        }

    }
    private void OnDestroy()
    {
        testEvent -= TestEvent;
    }
}