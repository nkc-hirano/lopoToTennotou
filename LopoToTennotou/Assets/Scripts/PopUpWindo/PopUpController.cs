using Cysharp.Threading.Tasks;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    [SerializeField] GameObject[] prefabPopUpObj;   // 元となるオブジェクト
    [SerializeField] bool key;                      // 入力検知用

    [SerializeField] int number;                    // オブジェクト番号
    const int FRAMELATE = 60;                       // フレームレート数

    public async void PopUpObjController(int number)
    {
        // 描写するものを入る
        using (var win = new RePopUpObjColl(prefabPopUpObj[number]))
        {
            // 移動待機
            await UniTask.DelayFrame((int)(win.FadeTime * FRAMELATE));
            Debug.Log("入力受付開始");
            // 入力待機
            await UniTask.WaitWhile(() => !key);
        }
    }

    public async void PopUpWindoController(int number)
    {
        // 描写するものを入る
        using (var win = new RePopUpWindoColl(prefabPopUpObj[number]))
        {
            // 移動待機
            await UniTask.DelayFrame((int)(win.FadeTime * FRAMELATE));
            Debug.Log("入力受付開始");
            // 入力待機
            await UniTask.WaitWhile(() => !key);
        }
    }

    private void Update()
    {
        string keynum = Input.inputString;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // PopUp 引数は元となるオブジェクト番号
            PopUpObjController(number);
        }        
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // PopUp 引数は元となるオブジェクト番号
            PopUpWindoController(number);
        }
        else if (keynum.ToString() != "")
        {
            key = true;
            keynum = null;
        }
        else
        {
            key = false;
        }
    }
}