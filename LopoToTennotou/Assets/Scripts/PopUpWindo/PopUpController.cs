using Cysharp.Threading.Tasks;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    public static PopUpController instance;
    [SerializeField] GameObject[] prefabPopUpObj;   // 元となるオブジェクト
    [SerializeField] bool key;                      // 入力検知用

    [SerializeField] int number;                    // オブジェクト番号
    const int FRAMELATE = 60;                       // フレームレート数

    private void Awake()
    {
        instance = this;
    }
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
        if (keynum.ToString() != "")
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