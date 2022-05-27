using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using GameCore;
using Zenject;

public class PopUpStartStageNumberwindowLogic : MonoBehaviour
{
    public static PopUpStartStageNumberwindowLogic inatance; // 他で使えるように

    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject popUpStartStageNumberWindo;    // 元となるオブジェクト
    GameObject createStartStageNumberWindo;   // 作成するオブジェクト

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
    public void CreatePopUpStartStageNumberWindo()
    {
        createStartStageNumberWindo = Instantiate(popUpStartStageNumberWindo, canvas.transform);
    }
    public void SetChild(GameObject children)
    {
        Instantiate(children, createStartStageNumberWindo.transform);
    }

    public void PouUpSummonWind()
    {
        DOTween
          .To(() => speed = 1200, (x) => speed = x, 0f, fadeTime)
          .SetUpdate(true)
          .OnUpdate(BackWind);
    }
    async public UniTask PouUpRepatriationWind()
    {
        await DOTween
          .To(() => speed, (x) => speed = x, -1200, fadeTime / 2)
          .SetUpdate(true)
          .OnUpdate(BackWind)
          .ToUniTask();
        Debug.Log("移動終了");
        Debug.Log("削除");
        Destroy(createStartStageNumberWindo);
    }
    private void BackWind()
    {
        Debug.Log("移動中");
        createStartStageNumberWindo.transform.localPosition = new Vector2(speed, 0);
    }
}