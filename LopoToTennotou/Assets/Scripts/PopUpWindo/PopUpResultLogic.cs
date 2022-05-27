using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using GameCore;
using Zenject;

public class PopUpResultLogic : MonoBehaviour
{
    public static PopUpResultLogic inatance; // 他で使えるように

    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject popUpResultWindo;    // 元となるオブジェクト
    GameObject createResultWindo;   // 作成するオブジェクト

    float speed = 0;        // ゲームオブジェクトの移動移動

    float fadeTime = 1.5f; // ウィンドウは0.5f推奨

    public float FadeTime => fadeTime;

    [Inject]
    CoreStateController stateController;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        inatance = this;
    }
    public void CreatePopUpResultWindo()
    {
        createResultWindo = Instantiate(popUpResultWindo, canvas.transform);
    }
    public void SetChild(GameObject children)
    {
        Instantiate(children, createResultWindo.transform);
    }

    public void PouUpSummonWind()
    {
        DOTween
          .To(() => speed = 1200, (x) => speed = x, 0f,fadeTime)
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
        Destroy(createResultWindo);
        stateController.CoreStateTypeUpdateObserver.OnNext(CoreStateType.Goal);
        stateController.CoreStateTypeUpdateObserver.OnNext(CoreStateType.Final);
    }
    private void BackWind()
    {
        Debug.Log("移動中");
        createResultWindo.transform.localPosition = new Vector2(speed, 0);
    }
}