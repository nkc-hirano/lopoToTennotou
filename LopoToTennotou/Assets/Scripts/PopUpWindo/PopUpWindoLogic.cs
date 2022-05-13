using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PopUpWindoLogic : MonoBehaviour
{
    public static PopUpWindoLogic inatance; // 他で使えるように

    [SerializeField]
    GameObject canvas;      // 
    [SerializeField]
    GameObject popUpWindo;    // 元となるオブジェクト
    GameObject createWindo;   // 作成するオブジェクト

    float speed = 0;        // ゲームオブジェクトの移動移動

    // ↓デバッグ用↓
    [SerializeField]
    float fadeTime = 0.5f; // ウィンドウは0.5f推奨

    public float FadeTime => fadeTime;

    private void Awake()
    {
        inatance = this;
    }
    public void CreatePopUpWindo() 
    {
        createWindo = Instantiate(popUpWindo, canvas.transform);
    }
    public void SetChild(GameObject children)
    {
        Instantiate(children, createWindo.transform);
    }

    public void PouUpSummonWind()
    {
        DOTween
          .To(() => speed = 0, (x) => speed = x, 1f, fadeTime - fadeTime * 0.1f)
          .SetUpdate(true)
          .OnUpdate(BackWind);
    }
    async public UniTask PouUpRepatriationWind()
    {
        await DOTween
          .To(() => speed, (x) => speed = x, 0, fadeTime / 2)
          .SetUpdate(true)
          .OnUpdate(BackWind)
          .ToUniTask();
        Debug.Log("移動終了");
        Debug.Log("削除");
        Destroy(createWindo);
    }
    private void BackWind()
    {
        Debug.Log("移動中");
        createWindo.transform.localScale = new Vector2(speed, speed);
    }
}
