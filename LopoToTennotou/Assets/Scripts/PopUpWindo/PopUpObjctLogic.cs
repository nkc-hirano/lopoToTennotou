using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PopUpObjctLogic : MonoBehaviour
{
    public static PopUpObjctLogic inatance; // 他で使えるように

    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject popUpObj;
    GameObject createObj;

    float speed = 0;        // ゲームオブジェクトの移動移動

    // ↓デバッグ用↓
    [SerializeField]
    float fadeTime = 2.5f; // ポップアップは2.5f以下推奨

    public float FadeTime => fadeTime;

    private void Awake()
    {
        inatance = this;
    }

    public void CreatePopUpObj() 
    {
        createObj = Instantiate(popUpObj, canvas.transform);
    }
    public void SetChild(GameObject children) 
    {
        GameObject obj = Instantiate(children, createObj.transform);
        obj.transform.localPosition = new Vector3(470, 205);
    }
    public void PouUpSummonMove()
    {
        DOTween
          .To(() => speed = 350, (x) => speed = x, 0f, fadeTime)
          .SetUpdate(true)
          .OnUpdate(BackMove);
    }
    async public UniTask PouUpRepatriationMove()
    {
        await DOTween
          .To(() => speed, (x) => speed = x, 350f, fadeTime/2)
          .SetUpdate(true)
          .OnUpdate(BackMove)
          .ToUniTask();
        Debug.Log("移動終了");
        Debug.Log("削除");
        Destroy(createObj);
    }
    private void BackMove()
    {
        Debug.Log("移動中");
        createObj.transform.localPosition = new Vector2(speed, 0);
    }
}